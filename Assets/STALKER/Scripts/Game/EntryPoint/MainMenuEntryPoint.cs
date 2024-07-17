/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure;
using System.Collections.Generic;
using StalkerZero.Services;
using System.Collections;
using UnityEngine;

namespace StalkerZero
{
    public class MainMenuEntryPoint : MonoBehaviour, IEntryPoint
    {
        private DIContainer m_container;
        private UIMainMenuView m_uIMainMenuView;

        [SerializeField] private List<AudioClip> m_musicBackgrounds;
        public IEnumerator Intialization(DIContainer parentContainer)
        {
            m_container = parentContainer;

            RegisterService(m_container);
            RegisterViewModel(m_container);
            BindView(m_container);

            PlayMusicMenu();
            yield return null;
        }

        private void RegisterService(DIContainer container)
        {

        }

        private void RegisterViewModel(DIContainer container)
        {
            container.RegisterSingleton<IMainMenuViewModel>(factory => new MainMenuViewModel(factory));
        }

        private void BindView(DIContainer container)
        {
            var loadService = container.Resolve<LoadService>();


            //Bind UIMainMenuView
            var uIMeinMenuPrefab = loadService.LoadPrefab<UIMainMenuView>(LoadService.PREFAB_UI_MAIN_MENU);
            m_uIMainMenuView = Instantiate(uIMeinMenuPrefab);
            var uIRootViewModel = container.Resolve<IUIRootViewModel>();
            uIRootViewModel.AttachSceneUIStatic(m_uIMainMenuView);
            var mainMenuViewModel = container.Resolve<IMainMenuViewModel>();
            m_uIMainMenuView.Bind(mainMenuViewModel);
        }

        private void PlayMusicMenu()
        {
            var clip = m_musicBackgrounds[Random.Range(0, m_musicBackgrounds.Count)];
            var audioSource = transform.GetComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
