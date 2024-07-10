/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;
using UnityEngine;

namespace StalkerZero.Infrastructure.MVVM.Binders
{
    public class EmptyMethodBinder : MethodBinder
    {
        [SerializeField] protected Action<object> m_action;
        
        protected override IBinding BindInternal(IViewModel viewModel)
        {
            m_action = Delegate.CreateDelegate(typeof(Action<object>), viewModel, MethodName) as Action<object>;
            OnBind();

            return null;
        }

        public void Perform()
        {
            m_action?.Invoke(null);
            
        }

        public void Perform(Component sender)
        {
            m_action?.Invoke(sender);
        }

        protected virtual void OnBind() { }
    }
}
