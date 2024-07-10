/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure.MVVM.Binders;

namespace StalkerZero.Infrastructure.Reactive
{
    public interface IObservable<out T>
    {
        IBinding Subscribe(IObserver<T> observer);
        void Unsubscribe(IObserver<T> observer);
    }
}
