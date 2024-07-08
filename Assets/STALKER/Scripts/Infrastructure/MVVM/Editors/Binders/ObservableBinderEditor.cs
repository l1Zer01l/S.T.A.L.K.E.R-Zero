/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure.MVVM.Binders;
using StalkerZero.Infrastructure.Reactive;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace StalkerZero.Infrastructure.MVVM.Editors
{
    [CustomEditor(typeof(ObservableBinder), true)]
    public class ObservableBinderEditor : BinderEditor
    {
        private ObservableBinder m_binder;
        protected override void OnStart()
        {
            m_binder = target as ObservableBinder;
        }

        protected override void InspectorGUI()
        {
            DrawPropertyName();
        }

        private void DrawPropertyName()
        {
            var properties = new List<string>() { NONE };

            properties = properties.Concat(System.Type.GetType(ViewModelTypeFullName.stringValue).GetProperties()
                                   .Where(property => property.PropertyType.IsGenericType)
                                   .Where(property => IsValidProperty(property.PropertyType))
                                   .Select(property => property.Name)
                                   .OrderBy(name => name))
                                   .ToList();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("PropertyName: ");

            if (GUILayout.Button(string.IsNullOrEmpty(PropertyName.stringValue) ? NONE : PropertyName.stringValue, EditorStyles.popup))
            {
                var provider = CreateInstance<StringListSearchProvider>();
                provider.Init(properties.ToArray(), OnPressedSearch);
                SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)), provider);
            }

            EditorGUILayout.EndHorizontal();
        }

        private void OnPressedSearch(string newPropertyName)
        {

            PropertyName.stringValue = newPropertyName == NONE ? null : newPropertyName;
            serializedObject.ApplyModifiedProperties();
        }

        private bool IsValidProperty(System.Type propertyType)
        {
            if(propertyType.GetGenericArguments().First() != m_binder.ArgumentType)
                return false;

            return propertyType.GetInterfaces().Where(i => i.IsGenericType)
                                               .Any(i => typeof(IObservable<>).IsAssignableFrom(i.GetGenericTypeDefinition()) ||
                                                    typeof(IObservableCollection<>).IsAssignableFrom(i.GetGenericTypeDefinition()));
        }
    }
}
