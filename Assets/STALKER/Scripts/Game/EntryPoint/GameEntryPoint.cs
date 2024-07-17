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
            RegisterViewModel(m_rootContainer);
            BindView(m_rootContainer);

            //Init Coroutines
            m_coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(m_coroutines.gameObject);
            m_rootContainer.RegisterInstance(m_coroutines);
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

            //----------- After Load Service ------------------

            //Register AudioService
            var loadService = container.Resolve<LoadService>();
            var audioServicePrefab = loadService.LoadPrefab<AudioService>(LoadService.PREFAB_AUDIO_SERVICE);
            var audioService = Object.Instantiate(audioServicePrefab);
            Object.DontDestroyOnLoad(audioService);
            container.RegisterInstance(audioService);

        }

        private void RegisterViewModel(DIContainer container)
        {
            container.RegisterSingleton<IUIRootViewModel>(factory => new UIRootViewModel());
        }

        private void BindView(DIContainer container)
        {
            var loadService = container.Resolve<LoadService>();


            //Bind UIRootView
            var uIRootViewModel = container.Resolve<IUIRootViewModel>();
            var uIRootViewPrefab = loadService.LoadPrefab<UIRootView>(LoadService.PREFAB_UI_ROOT);
            m_uIRootView = Object.Instantiate(uIRootViewPrefab);
            Object.DontDestroyOnLoad(m_uIRootView.gameObject);
            m_uIRootView.Bind(uIRootViewModel);
        }

        private void OnLoadScene(Scene scene, LoadSceneMode mode)
        {
            var sceneName = scene.name;
            if (sceneName.Equals(SceneService.MAIN_MENU_SCENE))
                m_coroutines.StartCoroutine(LoadMainMenu());

            if (sceneName.Equals(SceneService.GAMEPLAY_SCENE))
                m_coroutines.StartCoroutine(LoadGamePlay());
        }

        private IEnumerator LoadAndStartMainMenu()
        {       
            var sceneService = m_rootContainer.Resolve<SceneService>();
            yield return sceneService.LoadMenu();
        }

        private IEnumerator LoadMainMenu()
        {
            Time.timeScale = 0f;
            var uIRootViewModel = m_rootContainer.Resolve<IUIRootViewModel>();
            uIRootViewModel.ShowLoadingScreen();

            var mainMenuContainer = new DIContainer(m_rootContainer);

            var mainMenuEntryPoint = UnityExtention.GetEntryPoint<MainMenuEntryPoint>();
            yield return mainMenuEntryPoint.Intialization(mainMenuContainer);

            uIRootViewModel.HideLoadingScreen();
            Time.timeScale = 1f;
        }

        private IEnumerator LoadGamePlay()
        {
            Time.timeScale = 0f;
            var uIRootViewModel = m_rootContainer.Resolve<IUIRootViewModel>();
            uIRootViewModel.ShowLoadingScreen();

            var sceneService = m_rootContainer.Resolve<SceneService>();
            var gamePlayContainer = new DIContainer(sceneService.cachedContainer);

            var gamePlayEntryPoint = UnityExtention.GetEntryPoint<GamePlayEntryPoint>();
            yield return gamePlayEntryPoint.Intialization(gamePlayContainer);

            uIRootViewModel.HideLoadingScreen();
            Time.timeScale = 1f;
        }
    }
}
