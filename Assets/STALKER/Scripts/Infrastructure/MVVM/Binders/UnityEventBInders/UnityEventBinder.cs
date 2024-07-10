/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using UnityEngine;
using UnityEngine.Events;

namespace StalkerZero.Infrastructure.MVVM.Binders
{
    public abstract class UnityEventBinder<T> : ObservableBinder<T>
    {
        [SerializeField] private UnityEvent<T> m_event;
        [SerializeField] private UnityEvent<object, T> m_eventWithAnalitics;
        
        protected override void OnPropertyChanged(object sender, T newValue)
        {
            m_event?.Invoke(newValue);
            m_eventWithAnalitics?.Invoke(sender, newValue);
        }
    }
}
