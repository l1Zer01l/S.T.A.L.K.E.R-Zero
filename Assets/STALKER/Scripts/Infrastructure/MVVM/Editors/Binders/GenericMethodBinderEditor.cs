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
    [CustomEditor(typeof(GenericMethodBinder), true)]
    public class GenericMethodBinderEditor : MethodBinderEditor
    {
        private GenericMethodBinder m_genericMethodBinder;
        protected override void OnStart()
        {
            m_genericMethodBinder = target as GenericMethodBinder;
        }

        protected override IEnumerable<string> GetMethodNames()
        {
            var methodNames = new List<string>() { MVVMConstant.NONE };

            return methodNames.Concat(System.Type.GetType(ViewModelTypeFullName.stringValue).GetMethods()
                                     .Where(method => method.GetParameters().Length == 2 && method.ReturnType == typeof(void))
                                     .Where(method => method.GetCustomAttribute(typeof(ReactiveMethodAttribute)) is ReactiveMethodAttribute)
                                     .Where(method => method.GetParameters().First().ParameterType == typeof(object) &&
                                                      method.GetParameters().Last().ParameterType == m_genericMethodBinder.ArgumentType)
                                     .Select(property => property.Name)
                                     .OrderBy(name => name));
        }

        protected override string GetLabelField() => MVVMConstant.METHOD_NAME;
        
    }
}
