/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using UnityEngine;
using UnityEngine.Events;

namespace StalkerZero.Infrastructure.MVVM.Binders
{
    internal class ViewCollectionToUnityEventBinder : ObservableBinder<View>
    {
        [SerializeField] private UnityEvent<View> m_eventAdded;
        [SerializeField] private UnityEvent<View> m_eventRemoved;
        [SerializeField] private UnityEvent m_eventClear;

        protected override IBinding BindInternal(IViewModel viewModel)
        {
            return BindCollection(PropertyName, viewModel, OnAdded, OnRemoved, OnClear);
        }

        protected override void OnPropertyChanged(object sender, View newValue)
        {
            
        }

        private void OnAdded(View view)
        {
            m_eventAdded?.Invoke(view);
        }

        private void OnRemoved(View view) 
        {
            m_eventRemoved?.Invoke(view);
        }

        private void OnClear()
        {
            m_eventClear?.Invoke();
        }
    }
}
