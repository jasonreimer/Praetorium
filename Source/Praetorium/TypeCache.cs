using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace Praetorium
{
    public class TypeCache : ITypeCache
    {
        private const BindingFlags PublicInstanceScope = BindingFlags.Instance | BindingFlags.Public;

        private readonly ConcurrentDictionary<Type, TypeInfo> _typeInfos = new ConcurrentDictionary<Type, TypeInfo>();

        public PropertyInfo[] GetPublicReadonlyInstanceProperties(Type type)
        {
            return GetTypeInfo(type).PublicReadonlyInstanceProperties;
        }

        public PropertyInfo[] GetPublicInstanceProperties(Type type)
        {
            return GetTypeInfo(type).PublicInstanceProperties;
        }

        public MethodInfo[] GetPublicMethods(Type type)
        {
            return GetTypeInfo(type).PublicMethods;
        }

        private TypeInfo GetTypeInfo(Type type)
        {
            return _typeInfos.GetOrAdd(type, CreateTypeInfo);
        }

        private TypeInfo CreateTypeInfo(Type type)
        {
            var publicProps = type.GetProperties(PublicInstanceScope);
            var methods = type.GetMethods(PublicInstanceScope) ?? new MethodInfo[0];

            return new TypeInfo
            {
                Type = type,
                PublicInstanceProperties = publicProps,
                PublicReadonlyInstanceProperties = publicProps.Where(p => p.CanRead && p.GetMethod.IsPublic).ToArray(),
                PublicMethods = methods
            };
        }

        private class TypeInfo 
        {
            public Type Type;
            public PropertyInfo[] PublicReadonlyInstanceProperties { get; set; }
            public PropertyInfo[] PublicInstanceProperties { get; set; }
            public MethodInfo[] PublicMethods { get; set; }
        }
    }
}
