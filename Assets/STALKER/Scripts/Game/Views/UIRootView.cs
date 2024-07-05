/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using UnityEngine;

namespace StalkerZero
{
    public class UIRootView : MonoBehaviour
    {
        [SerializeField] private Transform m_loadingScreen;

        public void Awake()
        {
            HideLoadingScreen();
        }

        public void ShowLoadingScreen()
        {
            m_loadingScreen.gameObject.SetActive(true);
        }

        public void HideLoadingScreen()
        {
            m_loadingScreen.gameObject.SetActive(false);
        }
    }
}
