/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;
using UnityEngine;

namespace StalkerZero.Infrastructure.MVVM.Binders
{
    public class EmptyMethodBinder : MethodBinder
    {
        [SerializeField] private event Action m_action;
        protected override IDisposable BindInternal(IViewModel viewModel)
        {
            m_action = Delegate.CreateDelegate(typeof(Action), viewModel, MethodName) as Action;
            return null;
        }
    }
}
