/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure.MVVM;

namespace StalkerZero
{
    public interface IMainMenuViewModel : IViewModel
    {
        void ShowMainMenu();

        void HideMainMenu();
    }
}
