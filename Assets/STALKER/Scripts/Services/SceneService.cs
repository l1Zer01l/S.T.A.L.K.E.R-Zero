/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace StalkerZero.Services
{
    public class SceneService
    {
        public event UnityAction<Scene, LoadSceneMode> LoadSceneEvent 
        { 
            add => SceneManager.sceneLoaded += value;
            remove => SceneManager.sceneLoaded -= value;                                                       
        }

        public SceneService() 
        {
            
        }

        public string GetCurrentSceneName()
        {
            return SceneManager.GetActiveScene().name;
        }

        public void LoadMenu()
        {

        }
    }
}
