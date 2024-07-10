/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;
using UnityEngine;

namespace StalkerZero.Infrastructure.MVVM.Binders
{
    public abstract class OnTriggerGenericBinder<T> : GenericMethodBinder<T>
    {
        [SerializeField, HideInInspector] private string m_trigerViewTypeFullName;
        protected Type m_trigerViewType;

        private void Awake()
        {
#if UNITY_EDITOR
            transform.GetComponent<Rigidbody>().useGravity = false;
#endif
            OnAwake();
        }
        protected virtual void OnAwake() { }
        protected override void OnBind()
        {
            m_trigerViewType = Type.GetType(m_trigerViewTypeFullName);
        }
    }
}
