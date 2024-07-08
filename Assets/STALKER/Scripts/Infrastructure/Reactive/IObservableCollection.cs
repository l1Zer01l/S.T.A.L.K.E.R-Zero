/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;

namespace StalkerZero.Infrastructure.Reactive
{
    public interface IObservableCollection<T>
    {
        IDisposable Subscribe(IObserverCollection<T> observer);
        void Unsubscribe(IObserverCollection<T> observer);
    }
}
