/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using UnityEngine;

namespace StalkerZero.Infrastructure.MVVM.Binders
{
    public class VMToGameObjectCreation : Binder
    {
        [SerializeField] private View m_prefabView;
        protected override IBinding BindInternal(IViewModel viewModel)
        {
            var type = viewModel.GetType();
            if (type.FullName.Equals(ViewModelTypeFullName))
            {
                var propertyInfo = type.GetProperty(PropertyName);
                var subViewModel = propertyInfo.GetValue(viewModel) as IViewModel;
                var view = Instantiate(m_prefabView, transform);    
                view.Bind(subViewModel);
            }

            return null;
        }
    }
}
