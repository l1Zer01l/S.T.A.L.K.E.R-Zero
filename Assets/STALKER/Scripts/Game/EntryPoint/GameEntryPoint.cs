/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure;
using StalkerZero.Services;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StalkerZero
{
    public class GameEntryPoint
    {
        private static GameEntryPoint m_instance;

        private DIContainer m_rootContainer;
        private Coroutines m_coroutines;
        private UIRootView m_uIRootView;

        private GameEntryPoint()
        {
            m_rootContainer = new DIContainer();
            RegisterService(m_rootContainer);

            m_coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(m_coroutines.gameObject);
            m_rootContainer.RegisterInstance(m_coroutines);

            var loadService = m_rootContainer.Resolve<LoadService>();

            var m_uIRootViewPrefab = loadService.LoadPrefab<UIRootView>(LoadService.PREFAB_UI_ROOT);
            m_uIRootView = Object.Instantiate(m_uIRootViewPrefab);
            Object.DontDestroyOnLoad(m_uIRootView.gameObject);
            m_rootContainer.RegisterInstance(m_uIRootView);
        }

        public void Init()
        {
            var sceneService = m_rootContainer.Resolve<SceneService>();
            sceneService.LoadSceneEvent += OnLoadScene;

            if (sceneService.GetCurrentSceneName() != SceneService.BOOT_STRAP_SCENE)
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif

            m_coroutines.StartCoroutine(LoadAndStartMainMenu());
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Start()
        {
            m_instance = new GameEntryPoint();
            m_instance.Init();
        }

        private void RegisterService(DIContainer container)
        {
            container.RegisterSingleton(factory => new SceneService());

            container.RegisterSingleton(factory => new LoadService());
        }

        private void OnLoadScene(Scene scene, LoadSceneMode mode)
        {
            var sceneName = scene.name;
            if (sceneName.Equals(SceneService.MAIN_MENU_SCENE))
                m_coroutines.StartCoroutine(LoadMainMenu());
        }

        private IEnumerator LoadAndStartMainMenu()
        {       
            var sceneService = m_rootContainer.Resolve<SceneService>();
            yield return sceneService.LoadMenu();
        }

        private IEnumerator LoadMainMenu()
        {
            m_uIRootView.ShowLoadingScreen();

            var mainMenuContainer = new DIContainer(m_rootContainer);

            var mainMenuEntryPoint = UnityExtention.GetEntryPoint<MainMenuEntryPoint>();
            yield return mainMenuEntryPoint.Intialization(mainMenuContainer);

            m_uIRootView.HideLoadingScreen();
        }
    }
}
