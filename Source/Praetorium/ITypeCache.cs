using System;
using System.Reflection;

namespace Praetorium
{
    public interface ITypeCache
    {
        PropertyInfo[] GetPublicReadonlyInstanceProperties(Type type);
        PropertyInfo[] GetPublicInstanceProperties(Type type);
        MethodInfo[] GetPublicMethods(Type type);
    }
}
