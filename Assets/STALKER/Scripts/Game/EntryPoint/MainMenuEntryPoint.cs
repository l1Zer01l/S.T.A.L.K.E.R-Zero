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
        private UIMainMenuView m_uIMainMenuView;
        public IEnumerator Intialization(DIContainer parentContainer)
        {
            m_container = parentContainer;

            RegisterService(m_container);
            RegisterViewModel(m_container);
            BindView(m_container);

            yield return null;
        }

        private void RegisterService(DIContainer container)
        {

        }

        private void RegisterViewModel(DIContainer container)
        {
            container.RegisterSingleton<IMainMenuViewModel>(factory => new MainMenuViewModel());
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
    }
}
