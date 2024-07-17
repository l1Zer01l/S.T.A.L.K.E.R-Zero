/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure;
using StalkerZero.Infrastructure.MVVM;
using StalkerZero.Infrastructure.Reactive;
using StalkerZero.Services;

namespace StalkerZero
{
    public class MenuPanelViewModel : IViewModel
    {
        public SingleReactiveProperty<bool> IsOpenMenuPanel { get; private set; } = new();

        private SceneService m_sceneService;
        private DIContainer m_container;
        public MenuPanelViewModel(DIContainer container)
        {
            m_sceneService = container.Resolve<SceneService>();
            m_container = container;

            OpenMenuPanel(null);
        }

        [ReactiveMethod]
        public void StartGame(object sender)
        {
            var coroutines = m_container.Resolve<Coroutines>();
            coroutines.StartCoroutine(m_sceneService.LoadGame(m_container));
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
