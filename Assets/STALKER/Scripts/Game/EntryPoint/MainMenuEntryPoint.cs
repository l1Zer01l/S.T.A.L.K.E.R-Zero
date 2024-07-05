/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure;
using StalkerZero.Services;
using System.Collections;
using UnityEngine;

namespace StalkerZero
{
    public class MainMenuEntryPoint : MonoBehaviour, IEntryPoint
    {
        private DIContainer m_container;
        private UIMainMenuView m_uIMainMenu;
        private AudioSource m_audioSource;
        public IEnumerator Intialization(DIContainer parentContainer)
        {
            m_container = parentContainer;

            var loadService = m_container.Resolve<LoadService>();
            var uIMeinMenuPrefab = loadService.LoadPrefab<UIMainMenuView>(LoadService.PREFAB_UI_MAIN_MENU);
            m_uIMainMenu = Object.Instantiate(uIMeinMenuPrefab);

            var rootUI = m_container.Resolve<UIRootView>();
            rootUI.AttachSceneUIStatic(m_uIMainMenu.gameObject);

            m_audioSource = GetComponent<AudioSource>();
            m_audioSource.Play();

            yield return null;
        }
    }
}
