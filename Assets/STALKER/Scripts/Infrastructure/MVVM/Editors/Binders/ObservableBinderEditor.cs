/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure.MVVM.Binders;
using StalkerZero.Infrastructure.Reactive;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace StalkerZero.Infrastructure.MVVM.Editors
{
    [CustomEditor(typeof(ObservableBinder), true)]
    public class ObservableBinderEditor : BinderEditor
    {
        private ObservableBinder m_observableBinder;
        private List<string> m_properties;
        protected override void OnStart()
        {
            m_properties = new List<string>();
            m_observableBinder = target as ObservableBinder;
        }

        protected override void InspectorGUI()
        {
            DefinePropertyName();
            DrawPropertyName(m_properties.ToArray(), "Property Name: ");
        }

        private void DefinePropertyName()
        {
            m_properties = new List<string>() { NONE };

            m_properties = m_properties.Concat(System.Type.GetType(ViewModelTypeFullName.stringValue).GetProperties()
                                       .Where(property => property.PropertyType.IsGenericType)
                                       .Where(property => IsValidProperty(property.PropertyType))
                                       .Select(property => property.Name)
                                       .OrderBy(name => name))
                                       .ToList();
        }

        private bool IsValidProperty(System.Type propertyType)
        {
            if(propertyType.GetGenericArguments().First() != m_observableBinder.ArgumentType)
                return false;

            return propertyType.GetInterfaces().Where(i => i.IsGenericType)
                                               .Any(i => typeof(IObservable<>).IsAssignableFrom(i.GetGenericTypeDefinition()) ||
                                                    typeof(IObservableCollection<>).IsAssignableFrom(i.GetGenericTypeDefinition()));
        }
    }
}
