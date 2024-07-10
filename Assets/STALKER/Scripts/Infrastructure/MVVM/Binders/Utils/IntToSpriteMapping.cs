/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;
using UnityEngine;

namespace StalkerZero.Infrastructure.MVVM.Binders
{
    [Serializable]
    public class IntToSpriteMapping
    {
        [SerializeField] private int _value;
        [SerializeField] private Sprite m_sprite;

        public int Value => _value;
        public Sprite Sprite => m_sprite;
    }
}