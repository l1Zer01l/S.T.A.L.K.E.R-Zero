/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using UnityEngine;

namespace StalkerZero.Infrastructure.MVVM.Binders
{
    public class OnTriggerEnterEmptyBinder : TriggerOnTriggerEmptyBinder
    {      
        private void OnTriggerEnter(Collider other)
        {
            var view = other.GetComponent(m_trigerViewType);
            if (view)
            {
                m_action?.Invoke(view);
            }
        }
    }
}
