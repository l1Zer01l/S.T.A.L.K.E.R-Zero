/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure;
using StalkerZero.Infrastructure.MVVM;
using StalkerZero.Infrastructure.Reactive;
using StalkerZero.Services;
using System;

namespace StalkerZero
{
    public class MenuPanelViewModel : IViewModel
    {
        public Action OnOpenMenuSettings { get; set; }
        public SingleReactiveProperty<bool> IsOpenMenuPanel { get; private set; } = new();

        private SceneService m_sceneService;
        private Coroutines m_coroutines;
        public MenuPanelViewModel(DIContainer container)
        {
            m_sceneService = container.Resolve<SceneService>();
            m_coroutines = container.Resolve<Coroutines>();

            OpenMenuPanel(null);
        }

        [ReactiveMethod]
        public void StartGame(object sender)
        {
            m_coroutines.StartCoroutine(m_sceneService.LoadGame());
        }

        [ReactiveMethod]
        public void OpenMenuSettings(object sender)
        {
            CloseMenuPanel(sender);
            OnOpenMenuSettings?.Invoke();
        }

        [ReactiveMethod]
        public void OpenMenuPanel(object sender)
        {
            IsOpenMenuPanel.SetValue(sender, true);
        }

        [ReactiveMethod]
        public void CloseMenuPanel(object sender)
        {
            IsOpenMenuPanel.SetValue(sender, false);            
        }

        [ReactiveMethod]
        public void ExitGame(object sender)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
