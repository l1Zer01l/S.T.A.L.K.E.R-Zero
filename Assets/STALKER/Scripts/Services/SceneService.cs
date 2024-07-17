/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace StalkerZero.Services
{
    public class SceneService
    {
        public const string BOOT_STRAP_SCENE = "BootStrap";
        public const string MAIN_MENU_SCENE = "MainMenu";
        public const string GAMEPLAY_SCENE = "GamePlay";
        public DIContainer cachedContainer { get; private set; }

        public event UnityAction<Scene, LoadSceneMode> LoadSceneEvent 
        { 
            add => SceneManager.sceneLoaded += value;
            remove => SceneManager.sceneLoaded -= value;                                                       
        }

        public SceneService() 
        {
            cachedContainer = null;
        }

        public string GetCurrentSceneName()
        {
            return SceneManager.GetActiveScene().name;
        }

        public IEnumerator LoadMenu()
        {
            yield return LoadScene(BOOT_STRAP_SCENE);
            yield return LoadScene(MAIN_MENU_SCENE);
        }

        public IEnumerator LoadGame(DIContainer container)
        {
            cachedContainer = container;

            yield return LoadScene(BOOT_STRAP_SCENE);
            yield return LoadScene(GAMEPLAY_SCENE);
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
