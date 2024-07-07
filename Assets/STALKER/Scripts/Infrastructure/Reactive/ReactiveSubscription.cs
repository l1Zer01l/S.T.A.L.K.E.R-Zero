/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;

namespace StalkerZero.Infrastructure
{
    public class ReactiveSubscription<T> : IDisposable
    {
        private IReactiveProperty<T> m_reactiveOwner;
        private IObserver<T> m_observer;

        public ReactiveSubscription(IReactiveProperty<T> reactiveOwner, IObserver<T> observer)
        {
            m_reactiveOwner = reactiveOwner;
            m_observer = observer;
        }

        public void Dispose()
        {
            if (m_reactiveOwner is null)
                return;

            m_reactiveOwner.Unsubscribe(m_observer);
            m_observer.Dispose();
            m_reactiveOwner = null;
            m_observer = null;
        }
    }
}
