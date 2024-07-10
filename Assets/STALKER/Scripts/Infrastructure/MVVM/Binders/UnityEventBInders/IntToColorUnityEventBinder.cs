/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StalkerZero.Infrastructure.MVVM.Binders
{
    public class IntToColorUnityEventBinder : ObservableBinder<int>
    {
        [SerializeField] private List<IntToColorMapping> m_mappingColors = new();
        [SerializeField] private Color m_defaultColor = Color.white; 
        [SerializeField] private UnityEvent<Color> m_event;
        [SerializeField] private UnityEvent<object, Color> m_eventWithAnalitics;
        
        private Dictionary<int, Color> m_colorMap;

        private void Awake()
        {
            m_colorMap = new Dictionary<int, Color>();

            foreach (var colorMap in m_mappingColors)
            {
                m_colorMap[colorMap.Value] = colorMap.Color;
            }
        }

        protected override void OnPropertyChanged(object sender, int newValue)
        {
            if (m_colorMap.TryGetValue(newValue, out var result))
            {
                m_event?.Invoke(result);
                m_eventWithAnalitics?.Invoke(sender, result);
                return;
            }
            
            m_event?.Invoke(m_defaultColor);
            m_eventWithAnalitics?.Invoke(sender, m_defaultColor);
        }
    }
}