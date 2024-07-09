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
    [CustomEditor(typeof(GenericMethodBinder), true)]
    public class GenericMethodBinderEditor : BinderEditor
    {
        private GenericMethodBinder m_genericMethodBinder;
        private List<string> m_methodNames;
        protected override void OnStart()
        {
            m_methodNames = new List<string>();
            m_genericMethodBinder = target as GenericMethodBinder;
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
                                         .Where(method => method.GetParameters().Length == 2 && method.ReturnType == typeof(void))
                                         .Where(method => method.GetCustomAttribute(typeof(ReactiveMethodAttribute)) is ReactiveMethodAttribute)
                                         .Where(method => method.GetParameters().First().ParameterType == typeof(object) && 
                                                          method.GetParameters().Last().ParameterType == m_genericMethodBinder.ArgumentType)
                                         .Select(property => property.Name)
                                         .OrderBy(name => name))
                                         .ToList();         
        }
    }
}
