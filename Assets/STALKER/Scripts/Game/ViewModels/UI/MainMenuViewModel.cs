/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

namespace StalkerZero
{
    public class MainMenuViewModel : IMainMenuViewModel
    {
        public MenuPanelViewModel MenuPanelViewModel { get; private set; }
        public MenuSettingsViewModel MenuSettingsViewModel { get; private set; }
        public MainMenuViewModel()
        {
            MenuPanelViewModel = new MenuPanelViewModel();
            MenuSettingsViewModel = new MenuSettingsViewModel();
        }

    }
}
