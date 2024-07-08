/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;

namespace StalkerZero.Infrastructure.Reactive
{
    public interface IObserverCollection<in T> : IDisposable
    {
        void NotifyCollectionAdded(object sender, T newValue);
        void NotifyCollectionRemoved(object sender, T newValue);
        void NotifyCollectionClear(object sender);
    }
}
