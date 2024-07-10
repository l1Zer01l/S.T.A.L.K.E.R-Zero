/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using Unity.VisualScripting;
using UnityEngine;

namespace StalkerZero.Infrastructure
{
    public static class UnityExtention
    {
        public static IEntryPoint GetEntryPoint<T>() where T : MonoBehaviour, IEntryPoint
        {
            return Object.FindFirstObjectByType<T>();
        }
#if UNITY_EDITOR
        public static void AddComponentInEditor<T>(Transform transform) where T : Component
        {

            if (transform.GetComponent<T>() == null)
                transform.AddComponent<T>();

        }
#endif
    }
}
