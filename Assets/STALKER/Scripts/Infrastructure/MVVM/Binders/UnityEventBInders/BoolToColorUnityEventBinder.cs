/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using UnityEngine;
using UnityEngine.Events;

namespace StalkerZero.Infrastructure.MVVM.Binders
{
    public class BoolToColorUnityEventBinder : ObservableBinder<bool>
    {
        [SerializeField] private Color m_trueColor = Color.white;
        [SerializeField] private Color m_falseColor = Color.black;
        [SerializeField] private UnityEvent<Color> m_event;
        [SerializeField] private UnityEvent<object, Color> m_eventWithAnalitics;

        protected override void OnPropertyChanged(object sender, bool newValue)
        {
            if (newValue)
            {
                m_event?.Invoke(m_trueColor);
                m_eventWithAnalitics?.Invoke(sender, m_trueColor);
                return;
            }

            m_event?.Invoke(m_falseColor);
            m_eventWithAnalitics?.Invoke(sender, m_falseColor);
        }
    }
}
