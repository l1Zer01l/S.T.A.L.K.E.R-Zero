/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure.MVVM.Binders;
using UnityEditor;

namespace StalkerZero.Infrastructure.MVVM.Editors
{
    [CustomEditor(typeof(EmptyMethodBinder))]
    public class EmptyMethodBinderEditor : BinderEditor
    {
        protected override void InspectorGUI()
        {
            
        }
    }
}
