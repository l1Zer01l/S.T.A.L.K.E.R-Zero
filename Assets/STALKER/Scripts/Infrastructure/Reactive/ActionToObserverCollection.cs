/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;

namespace StalkerZero.Infrastructure.Reactive
{
    public static class ActionToObserverCollection
    {
        public static IObserverCollection<T> Map<T>(Action actionAdded, Action ActionRemoved, Action actionClear)
        {
            return new ObserverCollectionFromAction<T, Action, Action>(actionAdded, actionAdded, actionClear);
        }

        public static IObserverCollection<T> Map<T>(Action<T> actionAdded, Action<T> ActionRemoved, Action actionClear)
        {
            return new ObserverCollectionFromAction<T, Action<T>, Action>(actionAdded, actionAdded, actionClear);
        }

        public static IObserverCollection<T> Map<T>(Action<object, T> actionAdded, Action<object, T> ActionRemoved, Action<object> actionClear)
        {
            return new ObserverCollectionFromAction<T, Action<object, T>, Action<object>>(actionAdded, actionAdded, actionClear);
        }
    }

    internal class ObserverCollectionFromAction<T, TActionAddedAndRemove, TActionClear> : IObserverCollection<T> 
                                               where TActionAddedAndRemove : Delegate
                                               where TActionClear : Delegate
    {
        private TActionAddedAndRemove m_delegateAdded;
        private TActionAddedAndRemove m_delegateRemoved;
        private TActionClear m_delegateClear;
        private int m_countTemplate;
        public ObserverCollectionFromAction(TActionAddedAndRemove actionAdded, TActionAddedAndRemove actionRemoved, TActionClear actionClear)
        {
            m_delegateAdded = actionAdded;
            m_delegateRemoved = actionRemoved;
            m_delegateClear = actionClear;
            m_countTemplate = typeof(TActionAddedAndRemove).GetGenericArguments().Length;
        }

        
        public void Dispose()
        {

        }

        public void NotifyCollectionAdded(object sender, T newValue)
        {
            InvokeAction(m_delegateAdded, sender, newValue);
        }

        public void NotifyCollectionRemoved(object sender, T newValue)
        {
            InvokeAction(m_delegateRemoved, sender, newValue);
        }

        public void NotifyCollectionClear(object sender)
        {
            if(m_countTemplate.Equals(2))
                m_delegateClear?.Method.Invoke(m_delegateClear.Target, new object[] { sender });
            else
                m_delegateClear?.Method.Invoke(m_delegateClear.Target, new object[] { });
        }
        private void InvokeAction<TAction>(TAction action, object sender, T value) where TAction : Delegate
        {
            if (m_countTemplate.Equals(0))
                action?.Method.Invoke(action.Target, new object[] { });
            else if (m_countTemplate.Equals(1))
                action?.Method.Invoke(action.Target, new object[] { value });
            else if (m_countTemplate.Equals(2))
                action?.Method.Invoke(action.Target, new object[] { sender, value });
        }
    }
}
