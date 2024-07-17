/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure;
using StalkerZero.Infrastructure.MVVM;
using StalkerZero.Services;
using System.Collections;
using UnityEngine;

namespace StalkerZero
{
    public class GamePlayEntryPoint : MonoBehaviour, IEntryPoint
    {
        private DIContainer m_container;
        [SerializeField] private View m_viewTest;
        public IEnumerator Intialization(DIContainer parentContainer)
        {
            m_container = parentContainer;

            RegisterService(m_container);
            RegisterViewModel(m_container);
            BindView(m_container);

            //for test
            yield return new WaitForSecondsRealtime(1);       
        }

        private void RegisterService(DIContainer container)
        {

        }

        private void RegisterViewModel(DIContainer container)
        {
            
        }

        private void BindView(DIContainer container)
        {
            var uiRootViewModel = m_container.Resolve<IUIRootViewModel>();

            uiRootViewModel.AttachSceneUIStatic(m_viewTest);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                var sceneService = m_container.Resolve<SceneService>();
                var coroutines = m_container.Resolve<Coroutines>();
                coroutines.StartCoroutine(sceneService.LoadMenu());
            }
        }
    }
}
