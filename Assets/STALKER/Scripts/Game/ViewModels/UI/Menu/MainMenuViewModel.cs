/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure;

namespace StalkerZero
{
    public class MainMenuViewModel : IMainMenuViewModel
    {
        public MenuPanelViewModel MenuPanelViewModel { get; private set; }
        public MenuSettingsViewModel MenuSettingsViewModel { get; private set; }
        public MainMenuViewModel(DIContainer container)
        {
            MenuPanelViewModel = new MenuPanelViewModel(container);
            MenuSettingsViewModel = new MenuSettingsViewModel(container);
        }

    }
}
