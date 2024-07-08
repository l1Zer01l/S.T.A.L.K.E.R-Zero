/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;

namespace StalkerZero.Infrastructure.Reactive
{
    public interface IObserver<in T> : IDisposable
    {
        void NotifyObservableChanged(object sender, T value);
    }
}
