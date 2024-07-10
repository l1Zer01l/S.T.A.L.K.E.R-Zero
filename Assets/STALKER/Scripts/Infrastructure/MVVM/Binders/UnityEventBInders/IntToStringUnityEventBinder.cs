/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using UnityEngine;
using UnityEngine.Events;

namespace StalkerZero.Infrastructure.MVVM.Binders
{
    public class IntToStringUnityEventBinder : ObservableBinder<int>
    {
        [SerializeField] private string m_textBefore = string.Empty;
        [SerializeField] private string m_textAfter = string.Empty;
        [SerializeField] private UnityEvent<string> m_event;
        [SerializeField] private UnityEvent<object, string> m_eventWithAnalitics;
        protected override void OnPropertyChanged(object sender, int newValue)
        {
            var result = m_textBefore + newValue.ToString() + m_textAfter;
            m_event?.Invoke(result);
            m_eventWithAnalitics?.Invoke(sender, result);
        }
    }
}
