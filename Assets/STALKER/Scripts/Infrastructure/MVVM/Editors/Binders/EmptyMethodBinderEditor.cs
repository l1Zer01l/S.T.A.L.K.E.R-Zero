/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure.MVVM.Binders;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEditor;


namespace StalkerZero.Infrastructure.MVVM.Editors
{
    [CustomEditor(typeof(EmptyMethodBinder))]
    public class EmptyMethodBinderEditor : BinderEditor
    {
        private List<string> m_methodNames;

        protected override void OnStart()
        {
            m_methodNames = new List<string>();
        }
        protected override void InspectorGUI()
        {
            DefineMethodNames();
            DrawPropertyName(m_methodNames.ToArray(), "Method Name: ");
        }

        private void DefineMethodNames()
        {
            m_methodNames = new List<string>() { NONE };

            m_methodNames = m_methodNames.Concat(System.Type.GetType(ViewModelTypeFullName.stringValue).GetMethods()
                                         .Where(method => method.GetParameters().Length == 0 && method.ReturnType == typeof(void))
                                         .Where(method => method.GetCustomAttribute(typeof(ReactiveMethodAttribute)) is ReactiveMethodAttribute)
                                         .Select(property => property.Name)
                                         .OrderBy(name => name))
                                         .ToList();
        }
    }
}
