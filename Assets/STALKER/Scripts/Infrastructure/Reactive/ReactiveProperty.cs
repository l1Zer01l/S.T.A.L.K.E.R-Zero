/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;
using System.Collections.Generic;

namespace StalkerZero.Infrastructure.Reactive
{
    public class ReactiveProperty<T> : IReactiveProperty<T>
    {  
        public T Value => m_value;

        private T m_value;
        private List<IObserver<T>> m_observers;
        public ReactiveProperty()
        {
            m_value = default(T);
            m_observers = new List<IObserver<T>>();
        }

        public void SetValue(object sender, T value)
        {
            if (m_value.Equals(value))
                return;

            m_value = value;
            OnChanged(sender, value);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!m_observers.Contains(observer))
            {
                m_observers.Add(observer);
                return new ReactiveSubscription<T>(this, observer);
            }
            return null;
        }

        public void Unsubscribe(IObserver<T> observer)
        {
            if (!m_observers.Contains(observer))
            {
                m_observers.Remove(observer);
            }
        }
        private void OnChanged(object sender, T newValue)
        {
            foreach (var observer in m_observers)
                observer.NotifyObservableChanged(sender, newValue);
        }
    }
}
