/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using UnityEngine;

public class SettingsContainer : MonoBehaviour
{
    [SerializeField] private Transform m_audioSettings;
    [SerializeField] private Transform m_gameSettings;

    private void Start()
    {
        ShowGameSettings();
    }

    public void ShowAudioSettings()
    {
        HideSettings();
        m_audioSettings.gameObject.SetActive(true);
    }

    public void ShowGameSettings()
    {
        HideSettings();
        m_gameSettings.gameObject.SetActive(true);
    }

    private void HideSettings()
    {
        m_audioSettings.gameObject.SetActive(false);
        m_gameSettings.gameObject.SetActive(false);
    }

}
