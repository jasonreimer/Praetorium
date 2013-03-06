using System;

namespace Praetorium
{
    public static class NullHandlerExtensions
    {

        /// <summary>
        /// If the <paramref name="value"/> is null or cannot be converted to the
        /// type of <typeparamref name="T"/>, then the default value 
        /// of <typeparamref name="T"/> is returned.
        /// </summary>
        /// <typeparam name="T">A value type.</typeparam>
        /// <param name="value">A value that must be converted to the type of <typeparamref name="T"/>.</param>
        /// <returns>
        /// If <paramref name="value"/> is or can be converted to the type of <typeparamref name="T"/>, value is returned;
        /// otherwise, the default value of <typeparamref name="T"/> is returned.
        /// </returns>
        public static T ReplaceNullWithDefault<T>(this object value) where T : struct
        {
            if (value == null)
                return default(T);

            if (value is T)
                return (T)value;

            if (value is IConvertible)
            {
                try
                {
                    return (T)Convert.ChangeType(value, typeof(T), null);
                }
                catch
                {
                }
            }

            return default(T);
        }

        /// <summary>
        /// If the <paramref name="value"/> is null or cannot be converted to the
        /// type of <typeparamref name="T"/>, then the default value 
        /// of <typeparamref name="T"/> is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="replacementValueHandler"></param>
        /// <returns></returns>
        public static T ReplaceNullOrDefault<T>(this object value, Func<T> replacementValueHandler) where T : struct
        {
            if (value == null)
                return replacementValueHandler();

            if (value is T)
                return value.Equals(default(T)) ? replacementValueHandler() : (T)value;

            if (value is IConvertible)
            {
                try
                {
#if !SILVERLIGHT
                    T convertedValue = (T)Convert.ChangeType(value, typeof(T));
#else
                    T convertedValue = (T)Convert.ChangeType(value, typeof(T), null);
#endif
                    if (!convertedValue.Equals(default(T)))
                        return convertedValue;
                }
                catch
                {
                }
            }

            return replacementValueHandler();
        }

        /// <summary>
        /// Returns the <paramref name="replacementValue"/> if the <paramref name="value"/> is the default of 
        /// <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="replacementValue">The replacement value.</param>
        /// <returns>
        /// If the <paramref name="value"/> is the default value of <typeparamref name="T"/>, 
        /// <paramref name="replacementValue"/> is returned; otherwise, <paramref name="value"/>.
        /// </returns>
        public static T ReplaceDefault<T>(this T value, T replacementValue) where T : struct
        {
            return value.Equals(default(T)) ? replacementValue : value;
        }

        public static T ReplaceDefault<T>(this T value, Func<T> replacementValueHandler) where T : struct
        {
            return value.Equals(default(T)) ? replacementValueHandler() : value;
        }

    }
}
