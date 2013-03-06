using Praetorium.Utilities;
using System;
using System.Diagnostics;

namespace Praetorium
{
    public static class Ensure
    {

        /// <summary>
        /// Throws an ArgumentNullException if the <paramref name="argumentValue" /> is null.
        /// </summary>
        /// <param name="argumentValue">The value to check.</param>
        /// <param name="argumentName">
        /// The name of the parameter in the calling method or property.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="argumentValue" /> is null.
        /// </exception>
        public static void ArgumentNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
                throw Errors.ArgumentNull(argumentName);
        }

        /// <summary>
        /// Throws an ArgumentException if the <paramref name="argumentValue" /> is null, or an empty string.  
        /// This method ignores spaces.
        /// </summary>
        /// <param name="argumentValue">The string.</param>
        /// <param name="argumentName">
        /// The name of the parameter in the calling method or property.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="argumentValue" /> is null or empty.
        /// </exception>
        public static void ArgumentNotNullOrWhiteSpace(string argumentValue, string argumentName)
        {
            if (argumentValue.IsNullOrWhiteSpace())
                throw Errors.ArgumentNullOrEmpty(argumentName);
        }

        public static void EnumValueIsDefined<TEnum>(TEnum argumentValue, string argumentName)
        {
            var enumType = typeof(TEnum);

            if (!enumType.IsEnum)
                throw Errors.NotAnEnumType(argumentName);

            if (!Enum.IsDefined(typeof(TEnum), argumentValue))
                throw Errors.InvalidEnumValue(argumentName, enumType.Name);
        }

        /// <summary>
        /// Ensures that the type of the <paramref name="providedType" /> is compatible with 
        /// a given <see cref="expectedType"/>
        /// </summary>
        /// <param name="expectedType">The type that is expected by the calling method.</param>
        /// <param name="providedType">The type that was provided to the calling method.</param>
        /// <param name="argumentName">The name of the parameter in the calling method.</param>
        /// <exception cref="ArgumentException">
        /// Throw if the <paramref name="providedType" /> is not compatible with the
        /// <see cref="expectedType" />.
        /// </exception>
        public static void TypeSupported(Type expectedType, Type providedType, string argumentName)
        {
            if (!providedType.Is(expectedType))
                throw Errors.TypeNotSupported(argumentName, expectedType.FullName);
        }

        /// <summary>
        /// Throws an ArgumentNullException if the <paramref name="argumentExpression" /> is null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argumentExpression"></param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="argumentExpression" /> is null.
        /// </exception>
        public static void ArgumentNotNull<T>(Func<T> argumentExpression) where T : class
        {
            if (argumentExpression() == null)
                throw Errors.ArgumentNull(ReflectionUtility.GetArgumentName(argumentExpression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argumentExpression"></param>
        /// <param name="field"></param>
        public static void ArgumentNotNull<T>(Func<T> argumentExpression, ref T field) where T : class
        {
            T argument = argumentExpression();

            if (argument == null)
                throw Errors.ArgumentNull(ReflectionUtility.GetArgumentName(argumentExpression));

            field = argument;
        }

        /// <summary>
        /// Throws an ArgumentException if the value returned by the <paramref name="argumentExpression" /> is null, or an empty string.  
        /// This method ignores spaces.
        /// </summary>
        /// <param name="argumentExpression"></param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="argumentExpression" /> is null or empty.
        /// </exception>
        public static void ArgumentNotNullOrWhiteSpace(Func<string> argumentExpression)
        {
            if (argumentExpression().IsNullOrEmpty())
                throw Errors.ArgumentNullOrEmpty(ReflectionUtility.GetArgumentName(argumentExpression));
        }

        /// <summary>
        /// Throws an ArgumentException if the value returned by the <paramref name="argumentExpression" /> is null, or an empty string.  
        /// This method ignores spaces.
        /// </summary>
        /// <param name="argumentExpression"></param>
        /// <param name="field"></param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="argumentExpression" /> is null or empty.
        /// </exception>
        public static void ArgumentNotNullOrWhiteSpace(Func<string> argumentExpression, ref string field)
        {
            string argument = argumentExpression();

            if (argument.IsNullOrEmpty())
                throw Errors.ArgumentNullOrEmpty(ReflectionUtility.GetArgumentName(argumentExpression));

            field = argument;
        }

        public static void ArgumentNotDefault<T>(Func<T> argumentExpression)
            where T : struct
        {
            if (argumentExpression().Equals(default(T)))
                throw Errors.ArgumentNotDefault(ReflectionUtility.GetArgumentName(argumentExpression));
        }

        public static void ArgumentNotDefault<T>(Func<T> argumentExpression, ref T field)
            where T : struct
        {
            T value = argumentExpression();

            if (value.Equals(default(T)))
                throw Errors.ArgumentNotDefault(ReflectionUtility.GetArgumentName(argumentExpression));

            field = value;
        }

        public static void ArgumentNotNullOrDefault<T>(Func<Nullable<T>> argumentExpression)
            where T : struct
        {
            var value = argumentExpression();

            if (!value.HasValue || value.Value.Equals(default(T)))
                throw Errors.ArgumentNotNullOrDefault(ReflectionUtility.GetArgumentName(argumentExpression));
        }

        public static void ArgumentNotNullOrDefault<T>(Func<Nullable<T>> argumentExpression, ref Nullable<T> field)
            where T : struct
        {
            var value = argumentExpression();

            if (!value.HasValue || value.Value.Equals(default(T)))
                throw Errors.ArgumentNotNullOrDefault(ReflectionUtility.GetArgumentName(argumentExpression));

            field = value;
        }

        public static void EnumValueIsDefined<TEnum>(Func<TEnum> argumentExpression) where TEnum : struct
        {
            var enumType = typeof(TEnum);

            if (!enumType.IsEnum)
                throw Errors.NotAnEnumType("TEnum");

            if (!Enum.IsDefined(enumType, argumentExpression()))
                throw Errors.InvalidEnumValue(ReflectionUtility.GetArgumentName(argumentExpression), enumType.Name);
        }

        public static void TypeSupported<T>(Func<T> argumentExpression, Type expectedType)
        {
            ArgumentNotNull(() => expectedType);

            var value = (object)argumentExpression();

            if (value == null)
            {
                string argumentName = ReflectionUtility.GetArgumentName(argumentExpression);
                throw Errors.ArgumentNull(argumentName);
            }

            var providedType = value.GetType();

            if (!expectedType.IsAssignableFrom(providedType))
            {
                string argumentName = ReflectionUtility.GetArgumentName(argumentExpression);
                throw Errors.TypeNotSupported(argumentName, expectedType.FullName);
            }
        }

        public static T ReturnIsNotNull<T>(T value) where T : class
        {
            if (value == null)
            {
                var methodSignature = ReflectionUtility.GetMethodSignature(new StackFrame(1).GetMethod());

                throw Errors.ReturnValueIsNull(methodSignature);
            }

            return value;
        }

        public static string ReturnIsNotNullOrWhiteSpace(string value)
        {
            if (value.IsNullOrEmpty())
            {
                var methodSignature = ReflectionUtility.GetMethodSignature(new StackFrame(1).GetMethod());

                throw Errors.ReturnValueIsNullOrEmpty(methodSignature);
            }

            return value;
        }

        public static T ReturnIsNotDefault<T>(T value) where T : struct
        {
            if (value.Equals(default(T)))
            {
                var methodSignature = ReflectionUtility.GetMethodSignature(new StackFrame(1).GetMethod());

                throw Errors.ReturnValueIsTheDefault(methodSignature);
            }

            return value;
        }

    }
}
