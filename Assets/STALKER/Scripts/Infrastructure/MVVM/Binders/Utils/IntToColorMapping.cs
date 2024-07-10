/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;
using UnityEngine;

namespace StalkerZero.Infrastructure.MVVM.Binders
{
    [Serializable]
    public class IntToColorMapping
    {
        [SerializeField] private int _value;
        [SerializeField] private Color _color = Color.white;

        public int Value => _value;
        public Color Color => _color;
    }
}
