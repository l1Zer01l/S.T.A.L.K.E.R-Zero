/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StalkerZero.Infrastructure.MVVM.Binders
{
    public class IntToSpriteUnityEventBinder : ObservableBinder<int>
    {
        [SerializeField] private List<IntToSpriteMapping> m_mappingSprites = new();
        [SerializeField] private Sprite m_defaultColor;
        [SerializeField] private UnityEvent<Sprite> m_event;
        [SerializeField] private UnityEvent<object, Sprite> m_eventWithAnalitics;

        private Dictionary<int, Sprite> m_colorMap;

        private void Awake()
        {
            m_colorMap = new Dictionary<int, Sprite>();

            foreach (var sprite in m_mappingSprites)
            {
                m_colorMap[sprite.Value] = sprite.Sprite;
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