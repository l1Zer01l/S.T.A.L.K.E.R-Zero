/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure.MVVM;

namespace StalkerZero
{
    public interface IUIRootViewModel : IViewModel
    {
        void ShowLoadingScreen();
        void HideLoadingScreen();
        void AttachSceneUIStatic(View staticView);
        void AttachSceneUIDynamic(View dynamicView);
    }
}
