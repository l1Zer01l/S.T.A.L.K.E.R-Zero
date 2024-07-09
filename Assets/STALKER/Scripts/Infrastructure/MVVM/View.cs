/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure.MVVM.Binders;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StalkerZero.Infrastructure.MVVM
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public class View : MonoBehaviour
    {
        [SerializeField] private string m_viewModelTypeFullName;
        [SerializeField] private string m_viewModelPropertyName;
        [SerializeField] private bool m_isParentView;

        [SerializeField] private List<View> m_subViews = new List<View>();
        [SerializeField] private List<Binder> m_childBinders = new List<Binder>();

        public string ViewModelTypeFullName => m_viewModelTypeFullName;
        public string ViewModelPropertyName => m_viewModelPropertyName;
        public bool IsPaerntView => m_isParentView;
        public void Bind(IViewModel viewModel)
        {
            IViewModel targetViewModel;

            if (m_isParentView)
            {
                targetViewModel = viewModel;
            }
            else
            {
                var property = viewModel.GetType().GetProperty(m_viewModelPropertyName);
                targetViewModel = property.GetValue(viewModel) as IViewModel;
            }

            foreach (var subView in m_subViews)
            {
                subView.Bind(targetViewModel);
            }

            foreach (var binder in m_childBinders)
            {
                binder.Bind(targetViewModel);
            }
        }

        public void Destroy()
        {
            
            Destroy(gameObject);
        }

#if UNITY_EDITOR
        private void Start()
        {
            var parentTransform = transform.parent;

            if (parentTransform)
            {
                var parentView = parentTransform.GetComponentInParent<View>();

                if (parentView != null)
                {
                    parentView.RegisterView(this);
                }
            }
            
        }

        private void OnDestroy()
        {
            var parentTransform = transform.parent;

            if (parentTransform)
            {
                var parentView = parentTransform.GetComponentInParent<View>();

                if (parentView != null)
                {
                    parentView.RemoveView(this);
                }
            }
        }

        public void RegisterBinder(Binder binder)
        {
            if (!m_childBinders.Contains(binder))
            {
                m_childBinders.Add(binder);
            }
        }

        public void RemoveBinder(Binder binder)
        {
            m_childBinders.Remove(binder);
        }

        public bool IsValidSetup()
        {
            foreach (var childBinder in m_childBinders)
            {
                if (childBinder is null)
                {
                    return false;
                }
            }

            foreach (var subView in m_subViews)
            {
                if (subView is null)
                {
                    return false;
                }
            }

            return true;
        }

        [ContextMenu("Force Fix")]
        public void Fix()
        {
            m_childBinders.Clear();
            var allFoundChildBinders = gameObject.GetComponentsInChildren<Binder>(true);
            foreach (var foundChildBinder in allFoundChildBinders)
            {
                if (foundChildBinder.ViewModelTypeFullName == ViewModelTypeFullName)
                {
                    RegisterBinder(foundChildBinder);
                }
            }

            m_subViews.Clear();
            var allFoundSubViews = gameObject.GetComponentsInChildren<View>(true);
            foreach (var foundSubView in allFoundSubViews)
            {
                var parentView = foundSubView.GetComponentsInParent<View>().FirstOrDefault(c => !ReferenceEquals(c, foundSubView));

                if (ReferenceEquals(this, parentView))
                {
                    RegisterView(foundSubView);
                }
            }
        }

        private void RegisterView(View view)
        {
            if (!m_subViews.Contains(view))
            {
                m_subViews.Add(view);
            }
        }

        private void RemoveView(View view)
        {
            m_subViews.Remove(view);
        }
#endif
    }
}

