using System;
using System.Collections.Generic;
using System.Linq;

namespace Praetorium.Utilities
{
    /// <summary>
    /// Contains general utilty methods for working with strings, or converting objects to strings.
    /// </summary>
    public static class StringUtility
    {

        #region Static Fields

        internal static readonly string[] TrueValues = new string[] { Boolean.TrueString.ToLower(), Boolean.TrueString.ToLower().First().ToString(), "1", "yes", "y", "on" };
        internal static readonly string[] FalseValues = new string[] { Boolean.FalseString.ToLower(), Boolean.FalseString.ToLower().First().ToString(), "0", "no", "n", "off" };

        #endregion

        /// <summary>
        /// Trims the value and converts zero-length strings to null, or
        /// simply returns null if the value is null.
        /// </summary>
        /// <param name="value">
        /// A reference to an object which will be converted to a string reference.
        /// </param>
        /// <returns>Null, or the trimmed value of the ToString() of the object.</returns>
        public static string TrimToNull(object value)
        {
            if (value == null)
                return null;

            var buffer = value.ToString();

            return buffer.IsNullOrWhiteSpace() ? null : buffer.Trim();
        }

        public static string ReplaceNullOrEmpty(object value)
        {
            return ReplaceNullOrEmpty(value, "");
        }

        /// <summary>
        /// The functions checks the incoming value for null, and
        /// returns the defaultValue, or if the value is
        /// not null, then the value is trimmed and returned.
        /// </summary>
        /// <param name="value">The value to check for null.</param>
        /// <param name="defaultValue">A trimmed instance of value, or the defaultValue</param>
        /// <returns>
        /// A trimmed instance of value, or the defaultValue
        /// parameter value.
        /// </returns>
        public static string ReplaceNullOrEmpty(object value, string defaultValue)
        {
            return TrimToNull(value) ?? defaultValue;
        }

        public static TValue GetEnumValueByName<TEnumType, TValue>(string name)
        {
            return (TValue)ConversionUtility.SafeToEnum(typeof(TEnumType), name, default(TEnumType));
        }

        public static Dictionary<string, string> ParseKeyValueText(string text, char pairDelimiter = ';', char keyValueDelimiter = '=')
        {
            var dictionary = new Dictionary<string, string>();

            foreach (var keyValuePair in text.Split(pairDelimiter))
            {
                var keyValue = keyValuePair.Split(keyValueDelimiter);

                if (keyValue.Length == 2 && keyValue[0].IsNotNullOrWhiteSpace())
                {
                    var key = keyValue[0].Trim();
                    var value = keyValue[1].TrimToNull();

                    if (key != null)
                        dictionary.Add(key, value);
                }
            }

            return dictionary;
        }
    }
}
