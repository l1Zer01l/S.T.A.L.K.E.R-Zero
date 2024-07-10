/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace StalkerZero.Infrastructure.MVVM.Editors
{
    public abstract class OnTriggerMethodBinderEditor : MethodBinderEditor
    {
        private SerializedProperty m_trigerViewTypeFullName;
        private TypeCache.TypeCollection m_cachedViewTypes;
        protected Dictionary<string, string> m_viewNames;
        protected override void OnStart()
        {
            base.OnStart();
            m_viewNames = new Dictionary<string, string>();
            m_trigerViewTypeFullName = serializedObject.FindProperty(nameof(m_trigerViewTypeFullName));
        }
        protected override void UpInspectorGUI()
        {
            m_cachedViewTypes = TypeCache.GetTypesDerivedFrom<View>();

            DefineViewNames();
            DrawSearchView();
        }

        protected void DrawSearchView()
        {
            var options = m_viewNames.Keys.ToArray();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(MVVMConstant.TRIGGER_VIEW);

            if (GUILayout.Button(GetShortName(m_trigerViewTypeFullName.stringValue), EditorStyles.popup))
            {
                var provider = CreateInstance<StringListSearchProvider>();
                provider.Init(options, OnPressedSearch);
                SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)), provider);
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DefineViewNames()
        {
            var allViewModelTypes = m_cachedViewTypes.Where(type => type.IsClass && !type.IsAbstract)
                                                     .OrderBy(type => type.Name);
            m_viewNames.Clear();
            m_viewNames[MVVMConstant.NONE] = null;
            foreach (var viewModelType in allViewModelTypes)
            {
                m_viewNames[viewModelType.Name] = viewModelType.FullName;
            }
        }

        private void OnPressedSearch(string shortName)
        {
            m_trigerViewTypeFullName.stringValue = m_viewNames[shortName];
            serializedObject.ApplyModifiedProperties();
        }

        private string GetShortName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                return MVVMConstant.NONE;

            var type = GetViewModelType(fullName);
            return type?.Name ?? MVVMConstant.NONE;
        }

        protected Type GetViewModelType(string viewModelTypeFullName)
        {
            var type = m_cachedViewTypes.FirstOrDefault(t => t.FullName == viewModelTypeFullName);

            return type;
        }
    }
}
