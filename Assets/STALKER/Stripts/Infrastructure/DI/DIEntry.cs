/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;

namespace StalkerZero.Infrastructure
{
    public abstract class DIEntry
    {
        protected DIContainer m_container { get; }
        public DIEntry(DIContainer container)
        {
            m_container = container;
        }
        public T CreateFactory<T>()
        {
            return ((DIEntry<T>)this).CreateFactory();
        }
    }

    public abstract class DIEntry<T> : DIEntry
    {
        protected Func<DIContainer, T> m_factory { get; }
        public DIEntry(DIContainer container, Func<DIContainer, T> factory) : base(container)
        {
            m_factory = factory;
        }
        public abstract T CreateFactory();
    }
}
