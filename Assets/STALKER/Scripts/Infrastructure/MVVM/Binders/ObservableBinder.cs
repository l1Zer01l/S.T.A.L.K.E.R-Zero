/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure.Reactive;
using System;

namespace StalkerZero.Infrastructure.MVVM.Binders
{
    public abstract class ObservableBinder : Binder
    {
        
    }

    public abstract class ObservableBinder<T> : ObservableBinder
    {
        protected IDisposable BindObservable(string propertyName, IViewModel viewModel, Action<T> callback)
        {
            var propertyInfo = viewModel.GetType().GetProperty(propertyName);
            var observable = propertyInfo.GetValue(viewModel) as Reactive.IObservable<T>;
            var handle = observable.Subscribe(ActionToObserver.Map(callback));
            return handle;
        }

        protected IDisposable BindObservable(string propertyName, IViewModel viewModel, Action<object, T> callback)
        {
            var propertyInfo = viewModel.GetType().GetProperty(propertyName);
            var observable = propertyInfo.GetValue(viewModel) as Reactive.IObservable<T>;
            var handle = observable.Subscribe(ActionToObserver.Map(callback));
            return handle;
        }

        protected IDisposable BindCollection(string propertyName, IViewModel viewModel, Action<T> actionAdded, Action<T> actionRemoved, Action actionClear)
        {
            var propertyInfo = viewModel.GetType().GetProperty(propertyName);
            var observable = propertyInfo.GetValue(viewModel) as IObservableCollection<T>;
            var handle = observable.Subscribe(ActionToObserverCollection.Map(actionAdded, actionRemoved, actionClear));
            return handle;
        }

        protected IDisposable BindCollection(string propertyName, IViewModel viewModel, Action<object, T> actionAdded, Action<object, T> actionRemoved, Action<object> actionClear)
        {
            var propertyInfo = viewModel.GetType().GetProperty(propertyName);
            var observable = propertyInfo.GetValue(viewModel) as IObservableCollection<T>;
            var handle = observable.Subscribe(ActionToObserverCollection.Map(actionAdded, actionRemoved, actionClear));
            return handle;
        }
    }
}
