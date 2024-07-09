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
    [CustomEditor(typeof(View), true)]
    public class ViewEditor : Editor
    {
        private View m_view;
        private View m_parentView;

        private SerializedProperty m_viewModelTypeFullName;
        private SerializedProperty m_viewModelPropertyName;
        private SerializedProperty m_isParentView;

        private TypeCache.TypeCollection m_cachedViewModelTypes;
        private SerializedProperty m_subViews;
        private SerializedProperty m_childBinders;

        private Dictionary<string, string> m_viewModelNames;

        private void OnEnable()
        {
            m_viewModelNames = new Dictionary<string, string>();

            m_viewModelTypeFullName = serializedObject.FindProperty(nameof(m_viewModelTypeFullName));
            m_viewModelPropertyName = serializedObject.FindProperty(nameof(m_viewModelPropertyName));
            m_isParentView = serializedObject.FindProperty(nameof(m_isParentView));
            m_childBinders = serializedObject.FindProperty(nameof(m_childBinders));
            m_subViews = serializedObject.FindProperty(nameof(m_subViews));

            m_view = target as View;
            var transformParent = m_view.transform.parent;

            if (transformParent)
            {
                m_parentView = transformParent.GetComponentInParent<View>();
                SetParentViewBoolean(m_parentView == null);
                serializedObject.ApplyModifiedProperties();
            }
            else
            {
                SetParentViewBoolean(true);
                serializedObject.ApplyModifiedProperties();
            }
        }

        public override void OnInspectorGUI()
        {
            m_cachedViewModelTypes = TypeCache.GetTypesDerivedFrom<IViewModel>();

            if (!m_isParentView.boolValue)
            {
                m_viewModelTypeFullName.stringValue = GetChildViewModelType(m_parentView.ViewModelTypeFullName, m_view.ViewModelPropertyName)?.FullName;
                serializedObject.ApplyModifiedProperties();
                if (string.IsNullOrEmpty(m_parentView.ViewModelTypeFullName))
                {
                    EditorGUILayout.HelpBox(MVVMConstant.WARNING_PARENT_VIEW, MessageType.Warning);
                    return;
                }
            }
            
            DefineViewModels();
            DrawScriptTitle();

            //Search ViewModel
            if (m_isParentView.boolValue)
            {
                DrawSearch(MVVMConstant.VIEW_MODEL, GetShortName(m_viewModelTypeFullName.stringValue));
            }
            else
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.LabelField(MVVMConstant.VIEW_MODEL + GetShortName(m_parentView.ViewModelTypeFullName));
                EditorGUI.EndDisabledGroup();

                DrawSearch(MVVMConstant.SUB_VIEW_MODEL, string.IsNullOrEmpty(m_viewModelPropertyName.stringValue) ?  MVVMConstant.NONE : m_viewModelPropertyName.stringValue);
            }

            //Show info
            EditorGUI.BeginDisabledGroup(true);
            
            EditorGUILayout.PropertyField(m_isParentView);
            EditorGUILayout.PropertyField(m_subViews);
            EditorGUILayout.PropertyField(m_childBinders);

            EditorGUI.EndDisabledGroup();

            //Draw Button
            if (m_isParentView.boolValue)
                DrawButtonOpenViewModel(m_view.ViewModelTypeFullName);
            else
                DrawSubViewModelDebugButtons(m_parentView.gameObject, GetChildViewModelType(m_parentView.ViewModelTypeFullName, m_view.ViewModelPropertyName)?.FullName);
            
            if (!m_view.IsValidSetup())
                DrawFixButton();           
        }

        private void DefineViewModels()
        {
            if (m_isParentView.boolValue)
                DefineAllViewModels();  
            else
                DefineViewModelsInParent();
            
        }

        private void DefineAllViewModels()
        {
            var allViewModelTypes = m_cachedViewModelTypes.Where(type => type.IsClass && !type.IsAbstract)
                                                          .OrderBy(type => type.Name);
            m_viewModelNames.Clear();
            m_viewModelNames[MVVMConstant.NONE] = null;
            foreach (var viewModelType in allViewModelTypes)
            {
                m_viewModelNames[viewModelType.Name] = viewModelType.FullName;
            }

        }

        private void DefineViewModelsInParent()
        {
            var viewModelTypesInParent = GetViewModelType(m_parentView.ViewModelTypeFullName).GetProperties()
                                                         .Where(property => typeof(IViewModel).IsAssignableFrom(property.PropertyType));
            m_viewModelNames.Clear();
            m_viewModelNames[MVVMConstant.NONE] = null;
            foreach (var viewModelType in viewModelTypesInParent)
            {
                m_viewModelNames[viewModelType.Name] = viewModelType.Name;
            }
        }

        private void OnPressedSearchViewModel(string shortNameViewModel)
        {
            if (m_isParentView.boolValue)
            {
                m_viewModelTypeFullName.stringValue = m_viewModelNames[shortNameViewModel];
                serializedObject.ApplyModifiedProperties();
            }
            else
            {
                m_viewModelPropertyName.stringValue = m_viewModelNames[shortNameViewModel];
                serializedObject.ApplyModifiedProperties();
            }
        }

        private string GetShortName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                return MVVMConstant.NONE;

            var type = GetViewModelType(fullName);
            return type?.Name ?? MVVMConstant.NONE;
        }

        private void DrawScriptTitle()
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField(MVVMConstant.SCRIPT, MonoScript.FromMonoBehaviour((View)target), typeof(View), false);
            EditorGUI.EndDisabledGroup();
        }

        private void DrawButtonOpenViewModel(string viewModelTypeFullName)
        {
            if (!string.IsNullOrEmpty(viewModelTypeFullName))
            {
                var viewModelType = GetViewModelType(viewModelTypeFullName);

                if (viewModelType == null)
                    return;
                
                if (GUILayout.Button($"Open {viewModelType.Name}"))
                    OpenScript(viewModelType.Name);      
            }
        }

        private void DrawFixButton()
        {
            EditorGUILayout.HelpBox(MVVMConstant.WARNING_FIX, MessageType.Warning);

            if (GUILayout.Button(MVVMConstant.FIX))
                m_view.Fix();
        }

        private void OpenScript(string typeName)
        {
            var guids = AssetDatabase.FindAssets($"t: script {typeName}");

            if (guids.Length > 0)
            {
                var scriptPath = AssetDatabase.GUIDToAssetPath(guids[0]);

                EditorUtility.OpenWithDefaultApp(scriptPath);
            }
            else
            {
                Debug.LogError($"No script found for type: {typeName}");
            }
        }

        protected Type GetViewModelType(string viewModelTypeFullName)
        {
            var type = m_cachedViewModelTypes.FirstOrDefault(t => t.FullName == viewModelTypeFullName);

            return type;
        }

        private void DrawSearch(string labelField, string buttonLabel)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(labelField);
            if (GUILayout.Button(buttonLabel, EditorStyles.popup))
            {
                var provider = CreateInstance<StringListSearchProvider>();
                provider.Init(m_viewModelNames.Keys.ToArray(), OnPressedSearchViewModel);
                SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)), provider);
            }
            EditorGUILayout.EndHorizontal();
        }

        private void SetParentViewBoolean(bool isParentView)
        {
            if (Application.isPlaying)     
                return;
            
            m_isParentView.boolValue = isParentView;

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawPingParentViewButton(GameObject parentViewGo)
        {
            if (parentViewGo != null && GUILayout.Button(MVVMConstant.PARENT_VIEW))
            {
                EditorGUIUtility.PingObject(parentViewGo);
            }
        }

        private void DrawSubViewModelDebugButtons(GameObject parentViewGo, string viewModelTypeFullName)
        {
            EditorGUILayout.BeginHorizontal();

            DrawPingParentViewButton(parentViewGo);
            DrawButtonOpenViewModel(viewModelTypeFullName);

            EditorGUILayout.EndHorizontal();
        }

        private Type GetChildViewModelType(string parentViewModelTypeFullName, string propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                var parentViewModelType = GetViewModelType(parentViewModelTypeFullName);
                var property = parentViewModelType.GetProperty(propertyName);

                return property.PropertyType;
            }

            return null;
        }
    }
}
