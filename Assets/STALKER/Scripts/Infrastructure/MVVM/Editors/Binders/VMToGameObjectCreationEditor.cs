/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure.MVVM.Binders;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace StalkerZero.Infrastructure.MVVM.Editors
{
    [CustomEditor(typeof(VMToGameObjectCreation))]
    public class VMToGameObjectCreationEditor : BinderEditor
    {
        protected override IEnumerable<string> GetPropertyNames()
        {
            var properties = new List<string>() { MVVMConstant.NONE };

            return properties.Concat(System.Type.GetType(ViewModelTypeFullName.stringValue).GetProperties()
                             .Where(property => !property.PropertyType.IsGenericType)
                             .Where(property => typeof(IViewModel).IsAssignableFrom(property.PropertyType))
                             .Select(property => property.Name)
                             .OrderBy(name => name));
        }


    }
}
