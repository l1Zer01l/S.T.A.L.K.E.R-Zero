/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System;

namespace StalkerZero.Infrastructure.MVVM.Editors
{
    [CustomEditor(typeof(View))]
    public class ViewEditor : Editor
    {
        private const string NONE = "None";

        private SerializedProperty m_viewModelTypeFullName;
        private SerializedProperty m_isParentView;

        private SerializedProperty m_subViews;
        private SerializedProperty m_childBinders;

        private Dictionary<string, string> m_viewModelNames;

        private void OnEnable()
        {
            m_viewModelNames = new Dictionary<string, string>();
            m_viewModelTypeFullName = serializedObject.FindProperty(nameof(m_viewModelTypeFullName));
            m_isParentView = serializedObject.FindProperty(nameof(m_isParentView));
            m_subViews = serializedObject.FindProperty(nameof(m_subViews));
            m_childBinders = serializedObject.FindProperty(nameof(m_childBinders));
        }

        public override void OnInspectorGUI()
        {
            DefineViewModels();

            //Search ViewModel
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("ViewModel: ");
            if (GUILayout.Button(GetShortName(m_viewModelTypeFullName.stringValue), EditorStyles.popup))
            {
                var provider = CreateInstance<StringListSearchProvider>();
                provider.Init(m_viewModelNames.Keys.ToArray(), OnPressedSearch);
                SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)), provider);
            }

            EditorGUILayout.EndHorizontal();


            EditorGUI.BeginDisabledGroup(true);

            EditorGUILayout.PropertyField(m_isParentView);
            EditorGUILayout.PropertyField(m_subViews);
            EditorGUILayout.PropertyField(m_childBinders);

            EditorGUI.EndDisabledGroup();
        }

        private void DefineViewModels()
        {
            var allViewModelTypes = TypeCache.GetTypesDerivedFrom<IViewModel>()
                                             .Where(type => type.IsClass && !type.IsAbstract)
                                             .OrderBy(type => type.Name);
            m_viewModelNames.Clear();
            m_viewModelNames[NONE] = null;
            foreach (var viewModelType in allViewModelTypes)
            {
                m_viewModelNames[viewModelType.Name] = viewModelType.FullName;
            }

        }

        private void OnPressedSearch(string shortNameViewModel)
        {
            m_viewModelTypeFullName.stringValue = m_viewModelNames[shortNameViewModel];
            serializedObject.ApplyModifiedProperties();
        }

        private string GetShortName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                return NONE;

            var type = Type.GetType(fullName);
            return type?.Name ?? NONE;
        }

    }
}
