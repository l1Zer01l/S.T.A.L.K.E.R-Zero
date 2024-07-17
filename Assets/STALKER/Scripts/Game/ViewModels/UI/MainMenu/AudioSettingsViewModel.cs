/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure;
using StalkerZero.Infrastructure.MVVM;
using StalkerZero.Services;

namespace StalkerZero
{
    public class AudioSettingsViewModel : IViewModel
    {
        private AudioService m_audioService;
        public AudioSettingsViewModel(AudioService audioService) 
        {
            m_audioService = audioService;
        }

        [ReactiveMethod]
        public void ChangeVolume(object sender, float volume)
        {
            m_audioService.SetMenuMusicVolume(volume);
        }
    }
}
