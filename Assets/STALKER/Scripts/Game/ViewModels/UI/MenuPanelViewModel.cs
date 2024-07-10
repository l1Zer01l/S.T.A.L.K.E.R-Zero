/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure.MVVM;
using StalkerZero.Infrastructure.Reactive;

namespace StalkerZero
{
    public class MenuPanelViewModel : IViewModel
    {
        public SingleReactiveProperty<bool> IsOpenMenuPanel { get; private set; } = new();

        public MenuPanelViewModel()
        {
            OpenMenuPanel(null);
        }

        [ReactiveMethod]
        public void OpenMenuPanel(object sender)
        {
            IsOpenMenuPanel.SetValue(sender, true);
        }

        [ReactiveMethod]
        public void CloseMenuPanel(object sender)
        {
            IsOpenMenuPanel.SetValue(sender, false);
        }
    }
}
