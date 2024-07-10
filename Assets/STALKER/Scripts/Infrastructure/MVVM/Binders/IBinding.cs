/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;

namespace StalkerZero.Infrastructure.MVVM.Binders
{
    public interface IBinding : IDisposable
    {
        void Binded();
    }
}
