/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using UnityEngine;

namespace StalkerZero.Infrastructure.MVVM.Binders
{
    public abstract class TriggerOnTriggerEmptyBinder : OnTriggerEmptyBinder
    {
        protected override void OnAwake()
        {
#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlaying)
            {
                UnityExtention.AddComponentInEditor<BoxCollider>(transform);
                transform.GetComponent<BoxCollider>().isTrigger = true;
            }
#endif
        }
    }
}