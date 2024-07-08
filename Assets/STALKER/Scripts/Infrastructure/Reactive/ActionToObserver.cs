/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;

namespace StalkerZero.Infrastructure.Reactive
{
    public static class ActionToObserver
    {
        public static IObserver<T> Map<T>(Action callback)
        {
            return new ObserverFromAction<T, Action>(callback);
        }

        public static IObserver<T> Map<T>(Action<T> callback)
        {
            return new ObserverFromAction<T, Action<T>>(callback);
        }

        public static IObserver<T> Map<T>(Action<object, T> callback)
        {
            return new ObserverFromAction<T, Action<object, T>>(callback);
        }
    }

    internal class ObserverFromAction<T, TAction> : IObserver<T> where TAction : Delegate
    {
        private TAction m_delegate;
        private int m_countTemplate;
        public ObserverFromAction(TAction callback)
        {
            m_delegate = callback;
            m_countTemplate = typeof(TAction).GetGenericArguments().Length;
        }

        public void NotifyObservableChanged(object sender, T value)
        {
            if (m_countTemplate.Equals(0))
                m_delegate?.Method.Invoke(m_delegate.Target, new object[] { });
            else if (m_countTemplate.Equals(1))
                m_delegate?.Method.Invoke(m_delegate.Target, new object[] { value });
            else if (m_countTemplate.Equals(2))
                m_delegate?.Method.Invoke(m_delegate.Target, new object[] { sender, value });
        }
        public void Dispose()
        {

        }
    }
}
