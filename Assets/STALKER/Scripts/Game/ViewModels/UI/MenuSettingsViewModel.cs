/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure.MVVM;
using StalkerZero.Infrastructure.Reactive;

namespace StalkerZero
{
    public  class MenuSettingsViewModel : IViewModel
    {
        public SingleReactiveProperty<bool> IsOpenMenuSettings { get; private set; } = new();

        public MenuSettingsViewModel() 
        {
            CloseMenuSettings(null);
        }

        [ReactiveMethod]
        public void OpenMenuSettings(object sender)
        {
            IsOpenMenuSettings.SetValue(sender, true);
        }

        [ReactiveMethod]
        public void CloseMenuSettings(object sender)
        {
            IsOpenMenuSettings.SetValue(sender, false);
        }
    }
}
