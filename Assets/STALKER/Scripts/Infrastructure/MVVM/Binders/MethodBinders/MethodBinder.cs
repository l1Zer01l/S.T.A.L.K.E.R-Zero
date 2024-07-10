/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

namespace StalkerZero.Infrastructure.MVVM.Binders
{
    public abstract class MethodBinder : Binder
    {
        protected string MethodName => PropertyName;
    }
}
