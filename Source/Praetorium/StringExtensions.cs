using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Praetorium
{

    public static class StringExtensions
    {

        public static string ToNewString(this IEnumerable<char> chars)
        {
            return new string(chars is char[] ? (char[])chars : chars.ToArray());
        }

        /// <summary>
        /// Converts the string to title case.
        /// </summary>
        /// <param name="value">The string.</param>
        /// <returns></returns>
        public static string Capitalize(this string value)
        {
#if !SILVERLIGHT
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
#else
            //TODO: fix
            return "";
#endif
        }

        /// <summary>
        /// Replaces one or more format items in the given string with the string representation of a specified object.
        /// </summary>
        /// <param name="format">A composite format string</param>
        /// <param name="args">An object array that contains zero or more objects to format</param>
        /// <returns>
        /// The formatted string.
        /// </returns>
        public static string ToFormat(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNotNullOrEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotNullOrWhiteSpace(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static void IfNotNullOrWhiteSpace(this string value, Action<string> action)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                Ensure.ArgumentNotNull(() => action);
                action(value);
            }
        }

        /// <summary>
        /// Trims the value and converts zero-length strings to null, or
        /// simply returns null if the value is null.
        /// </summary>
        /// <param name="value">The string to trim to null.</param>
        /// <returns>
        /// Returns null if value is null, or a zero-length trimmed string, else
        /// returns value trimmed.
        /// </returns>
        public static string TrimToNull(this string value)
        {
            return value.IsNullOrWhiteSpace() ? null : value.Trim();
        }

        public static string SafeTrim(this string value)
        {
            return value == null ? "" : value.Trim();
        }

        /// <summary>
        /// Removes characters from the value parameter that are not digit characters.
        /// </summary>
        /// <param name="value">
        /// The string to check and clean.  If null reference is passed, it will be
        /// converted to a zero-length string.
        /// </param>
        /// <returns>Returns a string with </returns>
        public static string RemoveNonDigits(this string value)
        {
            return RemoveNonDigits(value, true);
        }

        /// <summary>
        /// Removes characters from the value parameter that are not digit characters.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="replaceNull"></param>
        /// <returns></returns>
        public static string RemoveNonDigits(this string value, bool replaceNull)
        {
            if (value.IsNullOrWhiteSpace())
                return replaceNull ? "" : null;

            return value.Where(Char.IsDigit).ToNewString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveNonDigitsOrLetters(this string value)
        {
            return RemoveNonDigitsOrLetters(value, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="replaceNull"></param>
        /// <returns></returns>
        public static string RemoveNonDigitsOrLetters(this string value, bool replaceNull)
        {
            if (value.IsNullOrWhiteSpace())
                return replaceNull ? "" : null;

            return value.Where(Char.IsLetterOrDigit).ToNewString();
        }

        /// <summary>
        /// The functions checks the incoming value for null, and converts it 
        /// to a zero-length String.  If the value is not null, then the 
        /// String is trimmed and returned.
        /// </summary>
        /// <param name="value">The string value to check for null.</param>
        /// <returns>Returns value trimmed, or a zero-length string.</returns>
        public static string ReplaceNullOrWhiteSpace(this string value)
        {
            return value.ReplaceNullOrWhiteSpace("");
        }

        /// <summary>
        /// The functions checks the incoming value for null or empty, and
        /// returns the defaultValue, or if the value is
        /// not null, then the value is trimmed and returned.
        /// </summary>
        /// <param name="value">The value to check for null.</param>
        /// <param name="defaultValue">The default value to use if value is null.</param>
        /// <returns>A trimmed instance of value, or the defaultValue parameter value.</returns>
        public static string ReplaceNullOrWhiteSpace(this string value, string defaultValue)
        {
            return value.IsNullOrWhiteSpace() ? defaultValue : value.Trim();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <param name="trim"></param>
        /// <returns></returns>
        public static string ReplaceNullOrWhiteSpace(this string value, string defaultValue, bool trim)
        {
            return value.IsNullOrWhiteSpace() ? defaultValue : trim ? value.Trim() : value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValueHandler"></param>
        /// <returns></returns>
        public static string ReplaceNullOrWhiteSpace(this string value, Func<string> defaultValueHandler)
        {
            return value.ReplaceNullOrWhiteSpace(defaultValueHandler, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValueHandler"></param>
        /// <returns></returns>
        public static string ReplaceNullOrWhiteSpace(this string value, Func<string> defaultValueHandler, bool trim)
        {
            return value.IsNullOrWhiteSpace() ? defaultValueHandler() : trim ? value.Trim() : value;
        }

        public static bool IsNumeric(this string value)
        {
            value = value.TrimToNull();

            if (value != null)
            {
                double result = 0d;
                return double.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture.NumberFormat, out result);
            }

            return false;
        }

        public static bool IsOnlyNumbers(this string value)
        {
            value = value.TrimToNull();

            return value == null ? false : value.All(char.IsNumber);
        }

        public static bool ContainsOneOf(this string value, params char[] chars)
        {
            if (value == null) return false;

            return value.Any(c => chars.Contains(c));
        }

        public static bool ContainsLower(this string value)
        {
            return value == null ? false : value.Any(char.IsLower);
        }

        public static bool ContainsUpper(this string value)
        {
            return value == null ? false : value.Any(char.IsUpper);
        }

        public static bool ContainsDigit(this string value)
        {
            return value == null ? false : value.Any(char.IsDigit);
        }

        public static bool ContainsLetter(this string value)
        {
            return value == null ? false : value.Any(char.IsLetter);
        }

        public static bool ContainsNumber(this string value)
        {
            return value == null ? false : value.Any(char.IsNumber);
        }

        public static bool ContainsWhiteSpace(this string value)
        {
            return value == null ? false : value.Any(char.IsWhiteSpace);
        }

        public static bool ContainsAny(this string value, params char[] chars)
        {
            return value == null ? false : value.Any(c => chars.Contains(c));
        }

        public static bool IsOnlyWhiteSpace(this string value)
        {
            return value == null ? false : value.All(char.IsWhiteSpace);
        }

        public static string EnsureEndsWith(this string theString, string value)
        {
            Ensure.ArgumentNotNull(() => theString);

            return theString.EndsWith(value) ? theString : theString + value;
        }


        /// <summary>
        /// Hides the characters in a string by replacing them with the <paramref name="hiddenChar" />, starting 
        /// from the left side of the string.
        /// </summary>
        /// <param name="text">The text to modify.</param>
        /// <param name="visibleCharCount">The number of characters to display.</param>
        /// <param name="hiddenChar">The char used to replace the existing chars.</param>
        /// <returns>
        /// the modified string.
        /// </returns>
        public static string HideCharsLeft(this string text, int visibleCharCount, char hiddenChar = '*')
        {
            Ensure.ArgumentNotNull(() => text);

            if (text.Length < visibleCharCount)
                return text;

            return text.Substring(text.Length - visibleCharCount, visibleCharCount)
                       .PadLeft(text.Length, hiddenChar);
        }

        public static string HideCharsRight(this string text, int visibleCharCount, char hiddenChar = '*')
        {
            Ensure.ArgumentNotNull(() => text);

            if (text.Length < visibleCharCount)
                return text;

            return text.Substring(text.Length - visibleCharCount, visibleCharCount)
                        .PadRight(text.Length, hiddenChar);
        }

        public static string Truncate(this string value, int length, string suffix)
        {
            Ensure.ArgumentNotNull(() => value);

            return value.Length <= length ? value : value.Substring(0, length) + suffix;
        }
    }
}
