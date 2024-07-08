/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System.Collections.Generic;

namespace StalkerZero.Infrastructure.Reactive
{
    public interface IReactiveCollection<T> : IObservableCollection<T>, ICollection<T>
    {
        void Add(object sender, T item);
        void Clear(object sender);
        bool Remove(object sender, T item);

    }
}
