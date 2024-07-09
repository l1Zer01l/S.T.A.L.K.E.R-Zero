/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;

namespace StalkerZero.Infrastructure.MVVM.Binders
{
    public abstract class GenericMethodBinder : MethodBinder
    {
        public abstract Type ArgumentType { get; }
    }

    public class GenericMethodBinder<T> : GenericMethodBinder
    {
        public override Type ArgumentType => typeof(T);

        private event Action<object, T> m_action;
        protected override IDisposable BindInternal(IViewModel viewModel)
        {
            m_action = Delegate.CreateDelegate(typeof(Action<object, T>), viewModel, MethodName) as Action<object, T>;
            return null;
        }

        public void Perform(object sender, T newValue)
        {
            m_action?.Invoke(sender, newValue);
        }
    }

    
}
