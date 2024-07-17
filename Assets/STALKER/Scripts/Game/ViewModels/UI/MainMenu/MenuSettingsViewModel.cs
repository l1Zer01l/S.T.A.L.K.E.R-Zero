/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure.MVVM;
using StalkerZero.Infrastructure.Reactive;
using StalkerZero.Services;
using System;

namespace StalkerZero
{
    public  class MenuSettingsViewModel : IViewModel
    {
        public Action OnCloseMenuSettings { get; set; }
        public SingleReactiveProperty<bool> IsOpenMenuSettings { get; private set; } = new();

        public AudioSettingsViewModel AudioSettingsViewModel { get; private set; }

        public MenuSettingsViewModel(AudioService audioService) 
        {
            AudioSettingsViewModel = new AudioSettingsViewModel(audioService);

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
            OnCloseMenuSettings?.Invoke();
        }
    }
}
