/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;

namespace StalkerZero.Infrastructure.Reactive
{
    public class ReactiveCollection<T> : IReactiveCollection<T>
    {
        public int Count => m_items.Count;

        public bool IsReadOnly => false;

        private List<T> m_items = new List<T>();
        private List<IObserverCollection<T>> m_observers = new ();

        public void Add(T item)
        {
            m_items.Add(item);
            OnAddedElement(null, item);
        }

        public void Add(object sender, T item)
        {
            m_items.Add(item);
            OnAddedElement(sender, item);
        }

        public void Clear()
        {
            m_items.Clear();
            OnClear(null);
        }

        public void Clear(object sender)
        {
            m_observers.Clear();
            OnClear(sender);
        }

        public bool Contains(T item)
        {
            return m_items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            m_items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return m_items.GetEnumerator();
        }

        public bool Remove(T item)
        {
            var result = m_items.Remove(item);
            if(result)
                OnRemoveElement(null, item);
            return result;
        }

        public bool Remove(object sender, T item)
        {
            var result = m_items.Remove(item);
            if (result)
                OnRemoveElement(sender, item);
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IDisposable Subscribe(IObserverCollection<T> observer)
        {
            if (!m_observers.Contains(observer))
            {
                m_observers.Add(observer);
                return new ReactiveSubscriptionCollection<T>(this, observer);
            }
            return null;
        }

        public void Unsubscribe(IObserverCollection<T> observer)
        {
            if (m_observers.Contains(observer))
            {
                m_observers.Remove(observer);
            }
        }

        private void OnAddedElement(object sender, T item)
        {
            foreach (var observer in m_observers)
            {
                observer.NotifyCollectionAdded(sender, item);
            }
        }

        private void OnRemoveElement(object sender, T item)
        {
            foreach (var observer in m_observers)
            {
                observer.NotifyCollectionRemoved(sender, item);
            }
        }

        private void OnClear(object sender)
        {
            foreach (var observer in m_observers)
            {
                observer.NotifyCollectionClear(sender);
            }
        }
    }
}
