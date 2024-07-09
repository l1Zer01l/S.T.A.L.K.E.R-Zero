/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure.MVVM.Binders;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEditor;

namespace StalkerZero.Infrastructure.MVVM.Editors
{
    [CustomEditor(typeof(EmptyMethodBinder))]
    public class EmptyMethodBinderEditor : MethodBinderEditor
    {
        protected override IEnumerable<string> GetMethodNames()
        {
            var methodNames = new List<string>() { MVVMConstant.NONE };
            return methodNames.Concat(System.Type.GetType(ViewModelTypeFullName.stringValue).GetMethods()
                              .Where(method => method.GetParameters().Length == 0 && method.ReturnType == typeof(void))
                              .Where(method => method.GetCustomAttribute(typeof(ReactiveMethodAttribute)) is ReactiveMethodAttribute)
                              .Select(property => property.Name)
                              .OrderBy(name => name));
        }

        protected override string GetLabelField() => MVVMConstant.METHOD_NAME;
    }
}
