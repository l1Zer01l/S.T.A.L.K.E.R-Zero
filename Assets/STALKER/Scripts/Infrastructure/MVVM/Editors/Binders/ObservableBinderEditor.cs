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
        protected override void OnStart()
        {
            m_observableBinder = target as ObservableBinder;
        }

        protected override IEnumerable<string> GetPropertyNames()
        {
            var properties = new List<string>() { MVVMConstant.NONE };

            return properties.Concat(System.Type.GetType(ViewModelTypeFullName.stringValue).GetProperties()
                             .Where(property => property.PropertyType.IsGenericType)
                             .Where(property => IsValidProperty(property.PropertyType))
                             .Select(property => property.Name)
                             .OrderBy(name => name));
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
