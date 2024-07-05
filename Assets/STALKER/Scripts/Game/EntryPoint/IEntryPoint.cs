/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure;
using System.Collections;

namespace StalkerZero
{
    public interface IEntryPoint
    {
        IEnumerator Intialization(DIContainer parentContainer);
    }
}
