/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure;
using System.Collections;
using UnityEngine;

namespace StalkerZero
{
    public class GamePlayEntryPoint : MonoBehaviour, IEntryPoint
    {
        private DIContainer m_container;
        public IEnumerator Intialization(DIContainer parentContainer)
        {
            m_container = parentContainer;

            RegisterService(m_container);
            RegisterViewModel(m_container);
            BindView(m_container);

            var mainMenu = m_container.Resolve<IMainMenuViewModel>();
            mainMenu.HideMainMenu();

            yield return null;
        }

        private void RegisterService(DIContainer container)
        {

        }

        private void RegisterViewModel(DIContainer container)
        {
            
        }

        private void BindView(DIContainer container)
        {
                       
        }    
    }
}
