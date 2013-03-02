using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Praetorium.Utilities
{
    /// <summary>
    /// Contains utility methods for working with the Reflection API.
    /// </summary>
    public static class ReflectionUtility
    {

        private const BindingFlags AllInstanceScopes = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        /// Creates the specified <typeparamref name="T"/> using any no argument constructor.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        /// A new instance of T.
        /// </returns>
        public static T Create<T>()
        {
            return GetNewDelegate<T>()();
        }

        /// <summary>
        /// Builds a delegate expression that invokes the default (parameterless) constructor
        /// of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// A type that has a default constructor defined.
        /// </typeparam>
        /// <returns>
        /// A delegate that returns a new instance of <typeparamref name="T"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when <typeparamref name="T"/> does not have a default constructor.
        /// </exception>
        public static Func<T> GetNewDelegate<T>()
        {
            var newExpression = Expression.New(typeof(T));
            var newT = Expression.Lambda<Func<T>>(newExpression).Compile();

            return newT;
        }

        /// <summary>
        /// Gets the name of the property from the specified <paramref name="propertyExpression"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The return type of the expression's delegate.
        /// </typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>
        /// The name of the property that the expression references.
        /// </returns>
        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return GetMemberExpression(propertyExpression).Member.Name;
        }

        public static string GetPropertyName<T, R>(Expression<Func<T, R>> propertyExpression)
        {
            return GetMemberExpression(propertyExpression).Member.Name;
        }

        /// <summary>
        /// Gets the property from the specified <paramref name="propertyExpression"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The return type of the expression's delegate.
        /// </typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>
        /// A <see cref="PropertyInfo"/> instance that relates to the property 
        /// referred to by the specified <paramref name="propertyExpression"/>.
        /// </returns>
        public static PropertyInfo GetProperty<T>(Expression<Func<T>> propertyExpression)
        {
            return (PropertyInfo)GetMemberExpression(propertyExpression).Member;
        }

        public static PropertyInfo GetProperty<T, R>(Expression<Func<T, R>> propertyExpression)
        {
            return (PropertyInfo)GetMemberExpression(propertyExpression).Member;
        }

        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        /// If the specified <paramref name="propertyName"/> is defined by the <paramref name="source"/> object,
        /// the value of the property will be returned; otherwise, null.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when any of the parameters are null.
        /// </exception>
        public static object GetPropertyValue(object source, string propertyName)
        {
            Ensure.ArgumentNotNull(() => source);
            Ensure.ArgumentNotNullOrWhiteSpace(() => propertyName);

            var property = source.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            return property == null ? null : property.GetValue(source, null);
        }

        /// <summary>
        /// Checks the <paramref name="source" />'s type for an instance method
        /// with the name of <paramref name="methodName" />.
        /// </summary>
        /// <param name="source">The object that is reflected.</param>
        /// <param name="methodName">The name of the method to check for.</param>
        /// <returns>
        /// true if a public or non-public instance method exists on the type with the 
        /// corresponding type; false if not.
        /// </returns>
        public static bool MethodExists(object source, string methodName)
        {
            return MethodExists(source.GetType(), methodName);
        }

        /// <summary>
        /// Returns the private event delegate field that corresponds to the <paramref name="eventName"/>.
        /// </summary>
        /// <param name="source">
        /// The object that declares the event of <paramref name="eventName"/>.
        /// </param>
        /// <param name="eventName">
        /// The name of the event.
        /// </param>
        /// <returns>
        /// The delegate field.
        /// </returns>
        public static Delegate GetEventDelegate(object source, string eventName)
        {
            return GetEventDelegate(source.GetType(), eventName);
        }

        /// <summary>
        /// Gets the constant strings.
        /// </summary>
        /// <typeparam name="TDeclaringType">The type of the declaring type.</typeparam>
        /// <returns></returns>
        public static string[] GetConstantStrings<TDeclaringType>()
        {
            return typeof(TDeclaringType).GetConstantStrings();
        }

        public static Dictionary<string, string> GetConstantStringsByName<TDeclaringType>()
        {
            return typeof(TDeclaringType).GetConstantStringsByName();
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <returns></returns>
        public static TAttribute GetAttribute<TAttribute, TSource>() where TAttribute : System.Attribute
        {
            return GetAttribute<TAttribute, TSource>(false);
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="includeInherited">if set to <c>true</c> [include inherited].</param>
        /// <returns></returns>
        public static TAttribute GetAttribute<TAttribute, TSource>(bool includeInherited) where TAttribute : System.Attribute
        {
            return typeof(TSource).GetAttribute<TAttribute>(includeInherited);
        }

#if !SILVERLIGHT
        /// <summary>
        /// Finds the type in current app domain.
        /// </summary>
        /// <param name="typeFullName">Full name of the type.</param>
        /// <returns>
        /// The type.
        /// </returns>
        public static Type FindTypeInCurrentAppDomain(string typeFullName)
        {
            Ensure.ArgumentNotNullOrWhiteSpace(() => typeFullName);

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var type = assembly.GetType(typeFullName, false, true);

                if (type != null)
                {
                    return type;
                }
            }

            return null;
        }
#endif



        /// <summary>
        /// Gets the method signature.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        public static string GetMethodSignature(MethodBase method)
        {
            Ensure.ArgumentNotNull(() => method);

            var builder = new StringBuilder();
            var declaringType = method.DeclaringType;

            if (declaringType != null)
            {
                builder.Append(declaringType.FullName.Replace('+', '.'));
                builder.Append(".");
            }

            builder.Append(method.Name);

            if ((method is MethodInfo) && ((MethodInfo)method).IsGenericMethod)
            {
                var genericArguments = ((MethodInfo)method).GetGenericArguments();
                builder.Append("[");
                var index = 0;
                var flag2 = true;

                while (index < genericArguments.Length)
                {
                    if (!flag2)
                    {
                        builder.Append(",");
                    }
                    else
                    {
                        flag2 = false;
                    }
                    builder.Append(genericArguments[index].Name);
                    index++;
                }

                builder.Append("]");
            }

            builder.Append("(");

            var parameters = method.GetParameters();
            var flag3 = true;

            for (int j = 0; j < parameters.Length; j++)
            {
                if (!flag3)
                {
                    builder.Append(", ");
                }
                else
                {
                    flag3 = false;
                }

                string name = "<UnknownType>";

                if (parameters[j].ParameterType != null)
                {
                    name = parameters[j].ParameterType.Name;
                }

                builder.Append(name + " " + parameters[j].Name);
            }

            builder.Append(")");

            return builder.ToString();
        }

        public static MemberExpression GetMemberExpression<T>(Expression<Func<T>> expression)
        {
            Ensure.ArgumentNotNull(() => expression);

            MemberExpression memberExpression = null;

            if (expression.Body.NodeType == ExpressionType.Convert)
            {
                var body = (UnaryExpression)expression.Body;
                memberExpression = body.Operand as MemberExpression;
            }
            else if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpression = expression.Body as MemberExpression;
            }

            if (memberExpression == null)
            {
                throw new ArgumentException("Not a member access expression.", "expression");
            }

            return memberExpression;
        }

        public static MemberExpression GetMemberExpression<T, R>(Expression<Func<T, R>> expression)
        {
            Ensure.ArgumentNotNull(() => expression);

            MemberExpression memberExpression = null;

            if (expression.Body.NodeType == ExpressionType.Convert)
            {
                UnaryExpression body = (UnaryExpression)expression.Body;
                memberExpression = body.Operand as MemberExpression;
            }
            else if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpression = expression.Body as MemberExpression;
            }

            if (memberExpression == null)
            {
                throw new ArgumentException("Not a member access expression.", "expression");
            }

            return memberExpression;
        }

        public static bool IsIndexedPropertyAccess(Expression expression)
        {
            Ensure.ArgumentNotNull(() => expression);

            return (IsMethodExpression(expression) && expression.ToString().Contains("get_Item"));
        }

        public static bool IsMethodExpression(Expression expression)
        {
            Ensure.ArgumentNotNull(() => expression);

            return ((expression is MethodCallExpression) || ((expression is UnaryExpression) && IsMethodExpression((expression as UnaryExpression).Operand)));
        }

        public static void SetProperty<TTarget, TReturn>(TTarget target, Expression<Func<TTarget, TReturn>> memberExpression, TReturn value) where TTarget : class
        {
            Ensure.ArgumentNotNull(() => target);
            Ensure.ArgumentNotNull(() => memberExpression);

            const BindingFlags nonPublicInstanceScope = BindingFlags.NonPublic | BindingFlags.Instance;

            PropertyInfo property = null;

            if (memberExpression.Body.NodeType == ExpressionType.MemberAccess)
            {
                var expression = memberExpression.Body as MemberExpression;

                if (expression.Member is PropertyInfo)
                {
                    property = (PropertyInfo)expression.Member;
                }
            }

            if (property == null)
            {
                throw new ArgumentException("The expression does not reference a property.", "memberExpression");
            }

            property = property.GetBaseProperty();

            property.SetValue(target, value, nonPublicInstanceScope, null, null, null);
        }

        public static void SetProperty<TTarget>(TTarget target, string propertyName, object value) where TTarget : class
        {
            Ensure.ArgumentNotNull(() => target);
            Ensure.ArgumentNotNullOrWhiteSpace(() => propertyName);

            const BindingFlags nonPublicInstanceScope = BindingFlags.NonPublic | BindingFlags.Instance;

            var entityType = typeof(TTarget);
            var property = entityType.GetBaseProperty(propertyName);

            property.SetValue(target, value, nonPublicInstanceScope, null, null, null);
        }

        public static MethodInfo GetMethod<T>(Expression<Action<T>> expression)
        {
            return GetMethod(expression.Body);
        }

        public static MethodInfo GetMethod<T, R>(Expression<Func<T, R>> expression)
        {
            return GetMethod(expression.Body);
        }

        public static string GetMethodName<T>(Expression<Action<T>> expression)
        {
            return GetMethod(expression.Body).Name;
        }

        public static string GetMethodName<T, R>(Expression<Func<T, R>> expression)
        {
            return GetMethod(expression.Body).Name;
        }

        public static MethodInfo GetMethod(Expression expression)
        {
            Ensure.ArgumentNotNull(() => expression);

            if (expression is MethodCallExpression)
            {
                return ((MethodCallExpression)expression).Method;
            }
            else
            {
                //TODO: resource exception message
                throw new ArgumentException("The expression must be a method call expression.", "expression");
            }
        }

        public static string GetArgumentName<T>(Func<T> argument)
        {
            string fieldName = null;
#if !SILVERLIGHT
            try
            {
                var methodBodyIL = argument.Method.GetMethodBody().GetILAsByteArray();

                // bytes 2-6 represent the argument/field handle
                var fieldHandle = BitConverter.ToInt32(methodBodyIL, 2);
                var module = argument.Target.GetType().Module;
                var fieldInfo = module.ResolveField(fieldHandle, argument.Target.GetType().GetGenericArguments(),
                                                    argument.Method.GetGenericArguments());
                fieldName = fieldInfo.Name;
            }
            catch
            {
            }
#endif
            return fieldName.ReplaceNullOrWhiteSpace("<unknown parameter>");
        }

    }
}
