/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using UnityEngine;
using UnityEngine.Audio;

namespace StalkerZero.Services
{
    public class AudioService : MonoBehaviour
    {
        private const string MENU_MUSIC_VOLUME = "MenuMusicVolume";
        private const string MENU_VOLUME = "MenuVolume";

        [SerializeField] private AudioMixerGroup m_audioMixer;

        public void SetMenuMusicVolume(float volume)
        {
            m_audioMixer.audioMixer.SetFloat(MENU_MUSIC_VOLUME, Mathf.Lerp(-80, 0, volume));
        }
    }
}
