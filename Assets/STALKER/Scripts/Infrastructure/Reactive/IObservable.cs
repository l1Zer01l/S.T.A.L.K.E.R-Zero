/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;

namespace StalkerZero.Infrastructure.Reactive
{
    public interface IObservable<T>
    {
        IDisposable Subscribe(IObserver<T> observer);
        void Unsubscribe(IObserver<T> observer);
    }
}
