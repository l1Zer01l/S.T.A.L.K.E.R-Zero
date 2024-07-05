/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using UnityEngine;

namespace StalkerZero.Infrastructure
{
    public static class UnityExtention
    {
        public static IEntryPoint GetEntryPoint<T>() where T : MonoBehaviour, IEntryPoint
        {
            return Object.FindFirstObjectByType<T>();
        }
    }
}
