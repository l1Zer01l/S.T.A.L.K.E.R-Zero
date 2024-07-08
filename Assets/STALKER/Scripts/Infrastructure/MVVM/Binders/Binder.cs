/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure.MVVM;
using System;
using UnityEngine;

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
        OnStart();
    }

    private void OnDestroy()
    {
        m_binding?.Dispose();

        OnDestroyed();
    }


}
