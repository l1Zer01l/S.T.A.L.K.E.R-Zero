/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using UnityEngine;

namespace StalkerZero.Services
{
    public class LoadService
    {
        public const string PREFAB_UIROOT = "UI/UIRoot";
        public LoadService()
        {

        }

        public T LoadPrefab<T>(string path) where T : Object
        {
            var prefab = Resources.Load<T>(path);
            if (prefab is null)
            {
                Debug.LogError($"Can't load prefab from: {path}");
                throw new System.MethodAccessException();
            }
            return prefab;
        }
    }
}
