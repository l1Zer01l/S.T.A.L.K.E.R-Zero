/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure;
using StalkerZero.Infrastructure.MVVM;
using StalkerZero.Infrastructure.Reactive;

namespace StalkerZero
{
    public class MenuPanelViewModel : IViewModel
    {
        public SingleReactiveProperty<bool> IsOpenMenuPanel { get; private set; } = new();

        public MenuPanelViewModel(DIContainer container)
        {
            OpenMenuPanel(null);
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
