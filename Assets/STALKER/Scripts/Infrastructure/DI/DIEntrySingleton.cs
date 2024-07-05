/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;

namespace StalkerZero.Infrastructure
{
    public class DIEntrySingleton<T> : DIEntry<T>
    {
        private T m_instance;
        public DIEntrySingleton(DIContainer container, Func<DIContainer, T> factory) : base(container, factory) { }

        public override T CreateFactory()
        {
            if (m_instance is null)
                m_instance = m_factory(m_container);

            return m_instance;
        }
    }
}
