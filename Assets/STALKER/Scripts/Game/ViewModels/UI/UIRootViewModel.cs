/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure.MVVM;
using StalkerZero.Infrastructure.Reactive;

namespace StalkerZero
{
    public class UIRootViewModel : IUIRootViewModel
    {
        public ReactiveProperty<bool> IsActiveLoadingSceen { get; private set; } = new();

        public ReactiveCollection<View> UIStaticView { get; private set; } = new();

        public ReactiveCollection<View> UIDynamicView { get; private set; } = new();

        public UIRootViewModel()
        {

        }

        public void ShowLoadingScreen()
        {
            IsActiveLoadingSceen.SetValue(null, true);
        }

        public void HideLoadingScreen()
        {
            IsActiveLoadingSceen.SetValue(null, false);
        }

        public void AttachSceneUIStatic(View staticView)
        {
            UIStaticView.Clear();
            UIStaticView.Add(staticView);
        }

        public void AttachSceneUIDynamic(View dynamicView)
        {
            UIStaticView.Clear();
            UIDynamicView.Add(dynamicView);
        }


    }
}
