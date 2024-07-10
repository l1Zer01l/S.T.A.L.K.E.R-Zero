/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure.MVVM;
using UnityEngine;

namespace StalkerZero
{
    public class UIRootAttacher : MonoBehaviour
    {
        [SerializeField] private Transform m_staticUIContainer;
        [SerializeField] private Transform m_dynamicUIContainer;

        public void AttachSceneUIStatic(View uISceneStatic)
        {
            uISceneStatic.transform.SetParent(m_staticUIContainer, false);
        }

        public void AttachSceneUIDynamic(View uISceneDynamic)
        {
            uISceneDynamic.transform.SetParent(m_dynamicUIContainer, false);
        }

        public void ClearStaticUIStatic()
        {
            var childCount = m_staticUIContainer.childCount;
            for (int i = 0; i < childCount; i++)
                Destroy(m_staticUIContainer.GetChild(i).gameObject);
        }

        public void ClearStaticUIDynamic()
        {
            var childCount = m_dynamicUIContainer.childCount;
            for (int i = 0; i < childCount; i++)
                Destroy(m_dynamicUIContainer.GetChild(i).gameObject);
        }
    }

}