/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure.MVVM.Binders;
using System;

namespace StalkerZero.Infrastructure.Reactive
{
    public class ReactiveSubscriptionCollection<T> : IBinding
    {
        private IReactiveCollection<T> m_reactiveOwner;
        private IObserverCollection<T> m_observer;

        public ReactiveSubscriptionCollection(IReactiveCollection<T> reactiveOwner, IObserverCollection<T> observer)
        {
            m_reactiveOwner = reactiveOwner;
            m_observer = observer;
        }

        public void Binded()
        {
            
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
