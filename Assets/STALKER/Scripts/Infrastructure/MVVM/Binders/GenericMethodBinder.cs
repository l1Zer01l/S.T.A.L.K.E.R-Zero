/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;

namespace StalkerZero.Infrastructure.MVVM.Binders
{
    public abstract class GenericMethodBinder : MethodBinder
    {

    }

    public class GenericMethodBinder<T> : GenericMethodBinder
    {
        private event Action<object, T> m_action;
        protected override IDisposable BindInternal(IViewModel viewModel)
        {
            m_action = Delegate.CreateDelegate(typeof(Action<object, T>), viewModel, MethodName) as Action<object, T>;
            return null;
        }
    }
}
