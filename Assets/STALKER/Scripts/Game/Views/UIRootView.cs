/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using UnityEngine;

namespace StalkerZero
{
    public class UIRootView : MonoBehaviour
    {
        [SerializeField] private Transform m_loadingScreen;
        [SerializeField] private Transform m_staticUIContainer;
        [SerializeField] private Transform m_dynamicUIContainer;
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

        public void AttachSceneUIStatic(GameObject uISceneStatic)
        {
            ClearStaticUIStatic();
            uISceneStatic.transform.SetParent(m_staticUIContainer, false);
        }

        public void AttachSceneUIDynamic(GameObject uISceneDynamic)
        {
            ClearStaticUIDynamic();
            uISceneDynamic.transform.SetParent(m_dynamicUIContainer, false);
        }

        private void ClearStaticUIStatic()
        {
            var childCount = m_staticUIContainer.childCount;
            for (int i = 0; i < childCount; i++)
                Destroy(m_staticUIContainer.GetChild(i).gameObject);
        }
        private void ClearStaticUIDynamic()
        {
            var childCount = m_dynamicUIContainer.childCount;
            for (int i = 0; i < childCount; i++)
                Destroy(m_dynamicUIContainer.GetChild(i).gameObject);
        }
    }
}
