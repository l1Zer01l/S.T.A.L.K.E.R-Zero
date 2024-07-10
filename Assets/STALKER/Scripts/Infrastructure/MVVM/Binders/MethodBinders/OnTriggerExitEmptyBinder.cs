/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using UnityEngine;

namespace StalkerZero.Infrastructure.MVVM.Binders
{
    public class OnTriggerExitEmptyBinder : TriggerOnTriggerEmptyBinder
    {
        private void OnTriggerExit(Collider other)
        {
            var view = other.GetComponent(m_trigerViewType);
            if (view)
            {
                m_action?.Invoke(view);
            }
        }
    }
}