﻿/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure.MVVM.Binders;
using UnityEditor;

namespace StalkerZero.Infrastructure.MVVM.Editors
{
    [CustomEditor(typeof(Binder), true)]
    public abstract class BinderEditor : Editor
    {
        protected const string NONE = "None";
        protected SerializedProperty PropertyName => m_propertyName;
        protected SerializedProperty MethodName => m_propertyName;
        protected SerializedProperty ViewModelTypeFullName => m_viewModelTypeFullName;

        private Binder m_binder;
        private View m_parentView;
        private SerializedProperty m_viewModelTypeFullName;
        private SerializedProperty m_propertyName;

        private void OnEnable()
        {
            m_binder = target as Binder;
            m_parentView = m_binder.GetComponentInParent<View>();
            m_viewModelTypeFullName = serializedObject.FindProperty(nameof(m_viewModelTypeFullName));
            m_propertyName = serializedObject.FindProperty(nameof(m_propertyName));
            OnStart();
        }
        protected virtual void OnStart() { }
        public override void OnInspectorGUI()
        {
            if (!m_viewModelTypeFullName.stringValue.Equals(m_parentView.ViewModelTypeFullName))
            {
                m_viewModelTypeFullName.stringValue = m_parentView.ViewModelTypeFullName;
                serializedObject.ApplyModifiedProperties();
            }
            if (string.IsNullOrEmpty(m_viewModelTypeFullName.stringValue))
            {
                EditorGUILayout.HelpBox("Cannot find viewModel in view. Please check view!", MessageType.Warning);
                return;
            }
            base.OnInspectorGUI();

            InspectorGUI();
            
        }

        protected abstract void InspectorGUI();
        
    }
}