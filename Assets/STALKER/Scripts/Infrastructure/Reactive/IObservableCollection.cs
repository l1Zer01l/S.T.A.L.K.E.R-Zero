/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure.MVVM.Binders;
using System;

namespace StalkerZero.Infrastructure.Reactive
{
    public interface IObservableCollection<T>
    {
        IBinding Subscribe(IObserverCollection<T> observer);
        void Unsubscribe(IObserverCollection<T> observer);
    }
}
