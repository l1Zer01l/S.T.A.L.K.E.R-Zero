/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;
using UnityEngine;

namespace StalkerZero.Infrastructure.MVVM.Binders
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif

    public abstract class Binder : MonoBehaviour
    {
        [SerializeField, HideInInspector] private string m_viewModelTypeFullName;
        [SerializeField, HideInInspector] private string m_propertyName;

        public string ViewModelTypeFullName => m_viewModelTypeFullName;
        protected string PropertyName => m_propertyName;

        private IDisposable m_binding;
        protected virtual void OnDestroyed() { }
        protected virtual void OnStart() { }

        public void Bind(IViewModel viewModel)
        {
            m_binding = BindInternal(viewModel);
        }
        protected abstract IDisposable BindInternal(IViewModel viewModel);
        private void Start()
        {
#if UNITY_EDITOR
            var parentView = GetComponentInParent<View>();
            parentView.RegisterBinder(this);
#endif
            OnStart();
        }

        private void OnDestroy()
        {
#if UNITY_EDITOR
            var parentView = GetComponentInParent<View>();
            if (parentView)
            {
                parentView.RemoveBinder(this);
            }
#endif
            m_binding?.Dispose();

            OnDestroyed();
        }


    }
}