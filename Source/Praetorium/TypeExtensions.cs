using Praetorium.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Praetorium
{
    public static class TypeExtensions
    {
        internal const BindingFlags AllInstanceScopes = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        internal const BindingFlags AllNonPublicScopres = BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;

        /// <summary>
        /// Creates an instance of the <paramref name="type"/> using reflection.
        /// </summary>
        /// <param name="type">
        /// The type to create.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters that matches one of the constructors of the type.
        /// </param>
        /// <returns>
        /// A new instance of the <paramref name="type"/>.
        /// </returns>
        public static object Create(this Type type, params object[] parameters)
        {
            Ensure.ArgumentNotNull(() => type);

            return Activator.CreateInstance(type, AllInstanceScopes, null, parameters, CultureInfo.CurrentCulture);
        }

        public static T CreateAs<T>(this Type type, params object[] parameters)
        {
            Ensure.ArgumentNotNull(() => type);

            return (T)Activator.CreateInstance(type, AllInstanceScopes, null, parameters, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Determines if the <paramref name="type"/> is a reference or nullable type.
        /// </summary>
        /// <param name="type">
        /// 
        /// </param>
        /// <returns>
        /// True if the type can be null; otherwise, false.
        /// </returns>
        public static bool IsNullable(this Type type)
        {
            if (type == null) return false;

            return type.IsInterface || type.IsClass || type.IsNullableType();
        }

        public static bool IsNullableType(this Type type)
        {
            if (type == null) return false;

            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

        public static bool IsNullableOf<T>(this Type type) where T : struct
        {
            if (type == null) return false;

            return typeof(T).IsNullableOf(type);
        }

        /// <summary>
        /// Determines if the <paramref name="type"/> the nullable of the specified <paramref name="valueType"/>.
        /// </summary>
        /// <param name="type">
        /// The type being checked.
        /// </param>
        /// <param name="valueType">
        /// The type being checked against.
        /// </param>
        /// <returns>
        /// True if <paramref name="type"/> is a nullable type of the <paramref name="valueType"/>.
        /// </returns>
        public static bool IsNullableOf(this Type type, Type valueType)
        {
            if (type == null) return false;

            return type.IsGenericTypeOf(typeof(Nullable<>)) && type.GetGenericArguments()[0].Equals(valueType);
        }

        public static bool IsStringConstant(this FieldInfo field)
        {
            if (field == null) return false;

            return field.IsLiteral && field.FieldType.IsString();
        }

        /// <summary>
        /// Uses reflection on the <paramref name="declaringType" />, and returns an array 
        /// of all of the public constants.
        /// </summary>
        /// <param name="declaringType">The type to reflect.</param>
        /// <returns>
        /// A string array containing the constant values for each public constant 
        /// field defined in the <paramref name="declaringType" />.
        /// </returns>
        public static string[] GetConstantStrings(this Type declaringType)
        {
            Ensure.ArgumentNotNull(() => declaringType);

            return declaringType.GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Where(IsStringConstant)
                    .Select(f => f.GetRawConstantValue().ToString())
                    .ToArray();
        }


        /// <summary>
        /// Uses reflection on the <paramref name="declaringType" />, and returns an dictionary of
        /// all of the public string constants.
        /// </summary>
        /// <param name="declaringType">The type to reflect.</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetConstantStringsByName(this Type declaringType)
        {
            Ensure.ArgumentNotNull(() => declaringType);

            return declaringType.GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Where(IsStringConstant)
                    .ToDictionary(f => f.Name, f => f.GetRawConstantValue().ToString());
        }


        /// <summary>
        /// Checks the <paramref name="sourceType" /> for an instance method
        /// with the name of <paramref name="methodName" />.
        /// </summary>
        /// <param name="sourceType">The type that is reflected for the method.</param>
        /// <param name="methodName">The name of the method to check for.</param>
        /// <returns>
        /// true if a public or non-public instance method exists on the type with the 
        /// corresponding type; false if not.
        /// </returns>
        public static bool MethodExists(this Type sourceType, string methodName)
        {
            Ensure.ArgumentNotNull(() => sourceType);
            Ensure.ArgumentNotNullOrWhiteSpace(() => methodName);

            return sourceType.GetMethod(methodName, AllInstanceScopes) != null;
        }

#if !SILVERLIGHT
        /// <summary>
        /// Finds the root stack frame relative to the <paramref name="rootType"/>, and 
        /// returns a new <see cref="T:System.Diagnostics.StackTrace"/> instance with 
        /// that frame at the top.
        /// </summary>
        /// <param name="rootType">Type of the root.</param>
        /// <returns></returns>
        public static StackTrace FindRootTrace(this Type rootType)
        {
            Ensure.ArgumentNotNull(() => rootType);

            var rootTrace = new StackTrace();
            int skipFrameCount = 0;

            foreach (var frame in rootTrace.GetFrames())
            {
                if (frame.GetMethod().DeclaringType != rootType)
                    skipFrameCount++;
                else
                    break;
            }

            if (skipFrameCount > 0)
                rootTrace = new StackTrace(skipFrameCount);

            return rootTrace;
        }
#endif

        /// <summary>
        /// Gets the <see cref="T:System.Reflection.PropertyInfo"/> for the 
        /// <paramref name="propertyName"/> from the declaring type of the 
        /// property.
        /// </summary>
        /// <param name="type">
        /// The type to inspect for the specified <paramref name="propertyName"/>.
        /// </param>
        /// <param name="propertyName">
        /// The name of the property.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Reflection.PropertyInfo"/> from 
        /// </returns>
        public static PropertyInfo GetBaseProperty(this Type type, string propertyName)
        {
            var property = type.GetProperty(propertyName, AllInstanceScopes);

            return property.GetBaseProperty();
        }

        public static MethodInfo GetPropertySetter(this PropertyInfo property)
        {
            var baseProperty = GetBaseProperty(property);

            if (baseProperty != null)
                return baseProperty.GetSetMethod(true);

            return null;
        }

        public static PropertyInfo GetBaseProperty(this PropertyInfo property)
        {
            if (property == null) 
                return null;

            if (property.DeclaringType == property.ReflectedType)
                return property;
            else
            {
                var baseProperty = property.DeclaringType.GetProperty(property.Name, AllInstanceScopes | BindingFlags.Static);

                return baseProperty == null ? property : baseProperty;
            }
        }

        public static bool IsOpenGeneric(this Type type)
        {
            if (type == null) return false;

            return type.IsGenericTypeDefinition || type.ContainsGenericParameters;
        }

        /// <summary>
        /// Gets the default (parameterless) constructor of the specified <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// The default constructor of the specified <paramref name="type"/>, or null if the 
        /// specified <paramref name="type"/> does not define one.
        /// </returns>
        public static ConstructorInfo GetDefaultCtor(this Type type)
        {
            return GetDefaultCtor(type, false);
        }

        /// <summary>
        /// Gets the default (parameterless) constructor of the specified <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="throwOnMissing">
        /// if set to <c>true</c>, an <see cref="ArgumentException"/> will be thrown if the type does not have a default constructor.
        /// </param>
        /// <returns>
        /// The default constructor of the specified <paramref name="type"/>, or null if the 
        /// specified <paramref name="type"/> does not define one.
        /// </returns>
        public static ConstructorInfo GetDefaultCtor(this Type type, bool throwOnMissing)
        {
            Ensure.ArgumentNotNull(() => type);

            var constructor = type.GetConstructor(AllInstanceScopes, null, Type.EmptyTypes, null);

            if (throwOnMissing && constructor == null)
                throw new ArgumentException(string.Format("Type '{0}' does not have a default constructor", type.FullName), "type");

            return constructor;
        }

        public static bool IsIntrinsic(this Type type)
        {
            Ensure.ArgumentNotNull(() => type);

            return type.IsValueType || type == typeof(string) || type == typeof(DBNull);
        }

        /// <summary>
        /// Determines whether the specified <paramref name="type"/> is an abstraction (i.e., an interface or abstract class).
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// 	<c>true</c> if the specified type is an abstraction; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAbstraction(this Type type)
        {
            Ensure.ArgumentNotNull(() => type);

            return type.IsInterface || type.IsAbstract;
        }

        public static bool IsConcreteTypeOf<T>(this Type type)
        {
            if (type == null) return false;

            return type.IsConcreteClass() && type.IsOrDerivesFrom<T>();
        }

        public static bool IsConcreteWithPublicDefaultCtor(this Type type)
        {
            if (type == null) return false;

            return type.IsConcrete() && type.HasPublicDefaultCtor();
        }

        public static bool IsConcreteWithDefaultCtor(this Type type)
        {
            if (type == null) return false;

            return type.IsConcrete() && type.HasDefaultCtor();
        }

        public static bool HasAttribute<TAttribute>(this Type type) where TAttribute : Attribute
        {
            if (type == null) return false;

            return type.GetAttribute<TAttribute>() != null;
        }

        public static bool HasDefaultCtor(this Type type)
        {
            return type.GetDefaultCtor() != null;
        }

        public static bool HasPublicDefaultCtor(this Type type)
        {
            return type.GetConstructor(new Type[] { }) != null;
        }

        /// <summary>
        /// Determines whether the specified <paramref name="type"/> is a a class and is not abstract.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <paramref name="type"/> is a a class and is not abstract; 
        /// 	otherwise, <c>false</c>.
        /// </returns>
        public static bool IsConcreteClass(this Type type)
        {
            if (type == null) return false;

            return type.IsClass && !type.IsAbstract;
        }

        /// <summary>
        /// Determines whether the <paramref name="type"/> is equal to or derives from the specified <paramref name="baseType"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="baseType">Type of the base.</param>
        /// <returns>
        /// 	<c>true</c> if is equal to or derives from the specified <paramref name="baseType"/>; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method is similar to the <see cref="M:Type.IsSubclassOf"/> method, but with one important distinction: 
        /// this method considers generic types.
        /// </remarks>
        public static bool IsOrDerivesFrom(this Type type, Type baseType)
        {
            if (type == null) return false;
            if (baseType == null) return false;

            return type.Is(baseType) || type.DerivesFrom(baseType);
        }

        /// <summary>
        /// Determines whether the <paramref name="type"/> is equal to or derives from the specified <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <typeparam name="TBaseType">Type of the base.</typeparam>
        /// <returns>
        /// 	<c>true</c> if is equal to or derives from the specified <paramref name="type"/>; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method is similar to the <see cref="M:Type.IsSubclassOf"/> method, but with one important distinction: 
        /// this method considers generic types.
        /// </remarks>
        public static bool IsOrDerivesFrom<TBaseType>(this Type type)
        {
            return IsOrDerivesFrom(type, typeof(TBaseType));
        }

        /// <summary>
        /// Determines whether the <paramref name="type"/> derives from the specified <paramref name="baseType"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="baseType">Type of the base.</param>
        /// <returns>
        /// 	<c>true</c> if the type derives from the specified <paramref name="baseType"/>; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method is similar to the <see cref="M:Type.IsSubclassOf"/> method, but with one important distinction: 
        /// this method considers generic types.
        /// </remarks>
        public static bool DerivesFrom(this Type type, Type baseType)
        {
            if (type == null || baseType == null || type == baseType) 
                return false;

            if (type.IsGenericTypeOf(baseType))
                return true;

            if (type.GetInterfaces().Any(i => i.Is(baseType) || i.IsGenericTypeOf(baseType)))
                return true;

            // recurse base types
            while (type.BaseType != null)
            {
                type = type.BaseType;
                if (type.Is(baseType) || type.IsGenericTypeOf(baseType))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Similar to the <c>is</c> keyword, which checks if a type is 
        /// compatible with the specified <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The target type.</typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool Is<T>(this Type type)
        {
            return Is(type, typeof(T));
        }

        /// <summary>
        /// Similar to the <c>is</c> keyword, which checks if a type is 
        /// compatible with the specified <paramref name="targetType"/>.
        /// </summary>
        /// <param name="type">The </param>
        /// <param name="targetType">The target type.</param>
        /// <returns></returns>
        public static bool Is(this Type type, Type targetType)
        {
            return targetType.IsAssignableFrom(type);
        }

        public static bool Closes(this Type type, Type openType)
        {
            if (type == null || openType == null || type == openType 
                || type.ContainsGenericParameters || !openType.ContainsGenericParameters)
                return false;

            if (type.IsGenericTypeOf(openType))
                return true;

            if (type.GetInterfaces().Any(i => i.IsGenericTypeOf(openType)))
                return true;

            // recurse base types
            while (type.BaseType != null)
            {
                type = type.BaseType;
                if (type.IsGenericTypeOf(openType))
                    return true;
            }

            return false;
        }

        public static bool FirstGenericParamIs(this Type type, Type paramType)
        {
            if (type == null) return false;

            return type.IsGenericType && type.GetGenericArguments().FirstOrDefault() == paramType;
        }

        public static bool IsGenericTypeOf(this Type type, Type genericTypeDefinition)
        {
            if (type == null) return false;

            return type.IsGenericType && type.GetGenericTypeDefinition() == genericTypeDefinition;
        }

        public static bool IsEnumerableOfT(this Type type)
        {
            if (type == null) return false;

            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }

        public static bool IsComplex(this Type type)
        {
            if (type == null) return false;

            return type.IsClass && type != typeof(string);
        }

        public static T CloseAndCreateAs<T>(this Type openType, params Type[] parameterTypes)
        {
            Ensure.ArgumentNotNull(() => openType);

            var closedType = openType.MakeGenericType(parameterTypes);

            return (T)Activator.CreateInstance(closedType);
        }

        public static bool IsString(this Type type)
        {
            if (type == null) return false;

            return type.Equals(typeof(string));
        }

        public static bool IsPrimitive(this Type type)
        {
            if (type == null) return false;

            return type.IsPrimitive && !IsString(type) && type != typeof(IntPtr);
        }

        public static bool IsSimple(this Type type)
        {
            if (type == null) return false;

            return type.IsPrimitive || IsString(type) || type.IsEnum;
        }

        public static bool IsConcrete(this Type type)
        {
            if (type == null) return false;

            return !type.IsAbstract && !type.IsInterface;
        }

        public static bool IsNotConcrete(this Type type)
        {
            if (type == null) return false;

            return !type.IsConcrete();
        }

        public static bool DoesNotDeriveFrom<TBaseType>(this Type type) 
        {
            if (type == null) return false;

            return !type.DerivesFrom<TBaseType>();
        }

        public static bool DoesNotDeriveFrom(this Type type, Type baseType)
        {
            if (type == null) return false;

            return !type.DerivesFrom(baseType);
        }

        /// <summary>
        /// Determines whether the <paramref name="type"/> derives from the specified <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <typeparam name="TBaseType">Type of the base.</typeparam>
        /// <returns>
        /// 	<c>true</c> if the type derives from the specified <paramref name="type"/>; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method is similar to the <see cref="M:Type.IsSubclassOf"/> method, but with one important distinction: 
        /// this method considers generic types.
        /// </remarks>
        public static bool DerivesFrom<TBaseType>(this Type type)
        {
            return DerivesFrom(type, typeof(TBaseType));
        }

        /// <summary>
        /// Returns the private event delegate field that corresponds to the <paramref name="eventName"/>.
        /// </summary>
        /// <param name="sourceType">
        /// The type that declares the event of <paramref name="eventName"/>.
        /// </param>
        /// <param name="eventName">
        /// The name of the event.
        /// </param>
        /// <returns>
        /// The delegate field.
        /// </returns>
        public static Delegate GetEventDelegate(this Type sourceType, string eventName)
        {
            Ensure.ArgumentNotNull(() => sourceType);
            Ensure.ArgumentNotNullOrWhiteSpace(() => eventName);

            var eventField = sourceType.GetField(eventName, AllNonPublicScopres);

            return eventField != null ? eventField.GetValue(null) as Delegate : null;
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public static TAttribute GetAttribute<TAttribute>(this ICustomAttributeProvider provider) where TAttribute : System.Attribute
        {
            return GetAttribute<TAttribute>(provider, false);
        }

        /// <summary>
        /// Determines whether the specified type is a non-string class or interface.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// 	<c>true</c> if the specified type is object; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsObject(this Type type)
        {
            return (type.IsClass || type.IsInterface) && type != typeof(string);
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="provider">The provider.</param>
        /// <param name="includeInherited">if set to <c>true</c> [include inherited].</param>
        /// <returns></returns>
        public static TAttribute GetAttribute<TAttribute>(this ICustomAttributeProvider provider, bool includeInherited) where TAttribute : System.Attribute
        {
            Ensure.ArgumentNotNull(() => provider);

            return provider.GetCustomAttributes(typeof(TAttribute), includeInherited)
                           .FirstOrDefault() as TAttribute;
        }

        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this ICustomAttributeProvider provider) where TAttribute : System.Attribute
        {
            return GetAttributes<TAttribute>(provider, false);
        }

        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this ICustomAttributeProvider provider, bool includeInherited) where TAttribute : System.Attribute
        {
            Ensure.ArgumentNotNull(() => provider);

            return (TAttribute[])provider.GetCustomAttributes(typeof(TAttribute), includeInherited);
        }

        /// <summary>
        /// Gets the default value.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <returns></returns>
        public static object GetDefaultValue(this Type sourceType)
        {
            Ensure.ArgumentNotNull(() => sourceType);

            return sourceType.IsValueType ? Activator.CreateInstance(sourceType) : null;
        }

        public static Type GetEnumerableType(this Type type)
        {
            Ensure.ArgumentNotNull(() => type);
            
            var genericEnumerableType = typeof(IEnumerable<>);

            if (!type.Closes(genericEnumerableType))
                //TODO: resource
                throw new ArgumentException("Type must derive from IEnumerable<T>", ReflectionUtility.GetArgumentName(() => type));

            if (type.IsArray)
                return type.GetElementType();
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == genericEnumerableType)
                return type.GetGenericArguments()
                           .First();
            else
                return type.GetInterfaces()
                           .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == genericEnumerableType)
                           .Select(x => x.GetGenericArguments()[0])
                           .First();
        }

        public static bool IsInAssemblyOf<T>(this Type type)
        {
            return type.Assembly.Equals(typeof(T).Assembly);
        }

        public static bool IsNotInAssemblyOf<T>(this Type type)
        {
            return !IsInAssemblyOf<T>(type);
        }

        public static bool IsInNamespaceOf<T>(this Type type)
        {
            return type.Namespace == typeof(T).Namespace;
        }

        public static bool IsNotInNamespaceOf<T>(this Type type)
        {
            return type.Namespace != typeof(T).Namespace;
        }

        public static bool IsEnumerable(this Type type)
        {
            return type.IsOrDerivesFrom<IEnumerable>();
        }

        public static bool IsAttribute(this Type type)
        {
            return type.IsSubclassOf(typeof(Attribute));
        }

        public static bool IsSubclassOf<TBase>(this Type type)
        {
            return type.IsSubclassOf(typeof(TBase));
        }

        public static bool IsCompilerGenerated(this ICustomAttributeProvider thing)
        {
            return thing.GetAttribute<CompilerGeneratedAttribute>() != null;
        }

        public static bool IsPrivate(this MemberInfo member)
        {
            Ensure.ArgumentNotNull(() => member);

            if (member is FieldInfo)
            {
                return ((FieldInfo)member).IsPrivate;
            }
            else if (member is PropertyInfo)
            {
                var property = ((PropertyInfo)member).GetBaseProperty();
                return property.CanRead ? property.GetGetMethod(true).IsPrivate : true
                    && property.CanWrite ? property.GetSetMethod(true).IsPrivate : true;
            }
            else if (member is MethodBase)
            {
                return ((MethodBase)member).IsPrivate;
            }
            else if (member is EventInfo)
            {
                var evt = (EventInfo)member;

                return evt.GetAddMethod(true).IsPrivate && evt.GetRemoveMethod(true).IsPrivate;
            }

            return false;
        }

        public static bool? IsVirtual(this MemberInfo member)
        {
            Ensure.ArgumentNotNull(() => member);

            if (member is MethodInfo)
            {
                return ((MethodInfo)member).IsVirtual;
            }
            else if (member is PropertyInfo)
            {
                var property = (PropertyInfo)member;
                return property.CanRead ? property.GetGetMethod().IsVirtual : true
                    && property.CanWrite ? property.GetSetMethod().IsVirtual : true;
            }
            else if (member is EventInfo)
            {
                var evt = (EventInfo)member;

                return evt.GetAddMethod().IsVirtual && evt.GetRemoveMethod().IsVirtual;
            }
            else
            {
                return null;
            }
        }

        public static Type GetMemberType(this MemberInfo memberInfo)
        {
            Ensure.ArgumentNotNull(() => memberInfo);

            if (memberInfo is MethodInfo)
                return ((MethodInfo)memberInfo).ReturnType;

            if (memberInfo is PropertyInfo)
                return ((PropertyInfo)memberInfo).PropertyType;

            if (memberInfo is FieldInfo)
                return ((FieldInfo)memberInfo).FieldType;

            return null;
        }

        public static bool IsIndexer(this PropertyInfo property)
        {
            if (property == null)
                return false;

            return property.GetIndexParameters().IsNotEmpty();
        }

        public static bool IsNotIndexer(this PropertyInfo property)
        {
            if (property == null)
                return false;

            return property.GetIndexParameters().IsEmpty();
        }
    }
}
