/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure.MVVM.Binders;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace StalkerZero.Infrastructure.MVVM.Editors
{
    [CustomEditor(typeof(GenericMethodBinder), true)]
    public class GenericMethodBinderEditor : BinderEditor
    {
        private GenericMethodBinder m_genericMethodBinder;
        protected override void OnStart()
        {
            m_genericMethodBinder = target as GenericMethodBinder;
        }
        protected override void InspectorGUI()
        {
            DrawPropertyName();
        }

        private void DrawPropertyName()
        {
            var properties = new List<string>() { NONE };

            properties = properties.Concat(System.Type.GetType(ViewModelTypeFullName.stringValue).GetMethods()
                                   .Where(method => method.GetParameters().Length == 2 && method.ReturnType == typeof(void))
                                   .Where(method => method.GetCustomAttribute(typeof(ReactiveMethodAttribute)) is ReactiveMethodAttribute)
                                   .Where(method => method.GetParameters().First().ParameterType == typeof(object) && 
                                                    method.GetParameters().Last().ParameterType == m_genericMethodBinder.ArgumentType)
                                   .Select(property => property.Name)
                                   .OrderBy(name => name))
                                   .ToList();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("MethodName: ");

            if (GUILayout.Button(string.IsNullOrEmpty(MethodName.stringValue) ? NONE : MethodName.stringValue, EditorStyles.popup))
            {
                var provider = CreateInstance<StringListSearchProvider>();
                provider.Init(properties.ToArray(), OnPressedSearch);
                SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)), provider);
            }

            EditorGUILayout.EndHorizontal();
        }

        private void OnPressedSearch(string newPropertyName)
        {
            MethodName.stringValue = newPropertyName == NONE ? null : newPropertyName;
            serializedObject.ApplyModifiedProperties();
        }
    }
}
