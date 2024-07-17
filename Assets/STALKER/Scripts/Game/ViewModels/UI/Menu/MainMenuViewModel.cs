/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure;
using StalkerZero.Infrastructure.Reactive;

namespace StalkerZero
{
    public class MainMenuViewModel : IMainMenuViewModel
    {
        public SingleReactiveProperty<bool> IsOpenMainMenu { get; private set; } = new();
        public MenuPanelViewModel MenuPanelViewModel { get; private set; }
        public MenuSettingsViewModel MenuSettingsViewModel { get; private set; }
        public MainMenuViewModel(DIContainer container)
        {
            IsOpenMainMenu.SetValue(null, true);
            MenuPanelViewModel = new MenuPanelViewModel(container);
            MenuSettingsViewModel = new MenuSettingsViewModel(container);
        }

        public void ShowMainMenu()
        {
            IsOpenMainMenu.SetValue(null, true);
        }

        public void HideMainMenu()
        {
            IsOpenMainMenu.SetValue(null, false);
        }
    }
}
