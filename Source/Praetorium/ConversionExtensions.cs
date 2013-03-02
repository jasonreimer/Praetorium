using System;

namespace Praetorium
{
    public static class ConversionExtensions
    {
        public static T As<T>(this object target)
        {
            return (T)target;
        }

        /// <summary>
        /// Converts the value type to the nullable version of the value type.
        /// </summary>
        /// <typeparam name="TValue">
        /// The type of the value type.
        /// </typeparam>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// A new nullable of the <typeparamref name="TValue"/> type.
        /// </returns>
        public static TValue? ToNullable<TValue>(this TValue value) where TValue : struct
        {
            return new Nullable<TValue>(value);
        }

    }
}
