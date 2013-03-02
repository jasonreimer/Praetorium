using System;
using System.Globalization;

namespace Praetorium.Utilities
{
    public static class ConversionUtility
    {

        /// <summary>
        /// Converts the <paramref name="value" /> to a DateTime, but if the
        /// conversion fails, DateTime.MinValue is returned.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <returns>
        /// The DateTime value, or DateTime.MinValue if the string could not be converted.
        /// </returns>
        public static DateTime SafeToDateTime(string value)
        {
            return SafeToDateTime(value, DateTime.MinValue, null, DateTimeStyles.AllowWhiteSpaces, null);
        }

        public static DateTime SafeToDateTime(string value, DateTime defaultValue)
        {
            return SafeToDateTime(value, defaultValue, null, DateTimeStyles.AllowWhiteSpaces, null);
        }

        public static DateTime SafeToDateTime(string value, DateTime defaultValue, DateTimeStyles dateTimeStyles)
        {
            return SafeToDateTime(value, defaultValue, null, dateTimeStyles, null);
        }

        public static DateTime SafeToDateTime(string value, DateTime defaultValue, DateTimeStyles dateTimeStyles, Func<DateTime, bool> predicate)
        {
            return SafeToDateTime(value, defaultValue, null, dateTimeStyles, predicate);
        }

        public static DateTime SafeToDateTime(string value, string format)
        {
            return SafeToDateTime(value, format, null, DateTime.MinValue);
        }

        public static DateTime SafeToDateTime(string value, string format, IFormatProvider provider, DateTime defaultValue)
        {
            if (value.IsNullOrWhiteSpace())
                return defaultValue;

            try
            {
                return DateTime.ParseExact(value, format, provider);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static DateTime SafeToDateTime(string value, DateTime defaultValue, IFormatProvider formatProvider, DateTimeStyles dateTimeStyles, Func<DateTime, bool> predicate)
        {
            DateTime parsedValue;

            if (DateTime.TryParse(value, formatProvider, dateTimeStyles, out parsedValue))
                if (predicate != null)
                    return predicate(parsedValue) ? parsedValue : defaultValue;
                else
                    return parsedValue;
            else
                return defaultValue;
        }

        public static byte SafeToByte(string value)
        {
            return SafeToByte(value, 0, NumberStyles.Any, null);
        }

        public static byte SafeToByte(string value, byte defaultValue)
        {
            return SafeToByte(value, defaultValue, NumberStyles.Any, null);
        }

        public static byte SafeToByte(string value, byte defaultValue, NumberStyles numberStyles)
        {
            return SafeToByte(value, defaultValue, numberStyles, null);
        }

        public static byte SafeToByte(string value, byte defaultValue, NumberStyles numberStyles, Func<byte, bool> predicate)
        {
            return SafeToNumber(value, defaultValue, numberStyles, predicate, byte.TryParse);
        }

        public static short SafeToInt16(string value)
        {
            return SafeToInt16(value, 0, NumberStyles.Any, null);
        }

        public static short SafeToInt16(string value, short defaultValue)
        {
            return SafeToInt16(value, defaultValue, NumberStyles.Any, null);
        }

        public static short SafeToInt16(string value, short defaultValue, NumberStyles numberStyles)
        {
            return SafeToInt16(value, defaultValue, numberStyles, null);
        }

        public static short SafeToInt16(string value, short defaultValue, NumberStyles numberStyles, Func<short, bool> predicate)
        {
            return SafeToNumber(value, defaultValue, numberStyles, predicate, short.TryParse);
        }

        public static int SafeToInt32(string value)
        {
            return SafeToInt32(value, 0, NumberStyles.Any, null);
        }

        public static int SafeToInt32(string value, int defaultValue)
        {
            return SafeToInt32(value, defaultValue, NumberStyles.Any, null);
        }

        public static int SafeToInt32(string value, int defaultValue, NumberStyles numberStyles)
        {
            return SafeToInt32(value, defaultValue, numberStyles, null);
        }

        public static int SafeToInt32(string value, int defaultValue, NumberStyles numberStyles, Func<int, bool> predicate)
        {
            return SafeToNumber(value, defaultValue, numberStyles, predicate, int.TryParse);
        }

        public static long SafeToInt64(string value)
        {
            return SafeToInt64(value, 0L, NumberStyles.Any, null);
        }

        public static long SafeToInt64(string value, long defaultValue)
        {
            return SafeToInt64(value, defaultValue, NumberStyles.Any, null);
        }

        public static long SafeToInt64(string value, long defaultValue, NumberStyles numberStyles)
        {
            return SafeToInt64(value, defaultValue, numberStyles, null);
        }

        public static long SafeToInt64(string value, long defaultValue, NumberStyles numberStyles, Func<long, bool> predicate)
        {
            return SafeToNumber(value, defaultValue, numberStyles, predicate, long.TryParse);
        }

        public static float SafeToSingle(string value)
        {
            return SafeToSingle(value, 0F, NumberStyles.Any, null);
        }

        public static float SafeToSingle(string value, float defaultValue)
        {
            return SafeToSingle(value, defaultValue, NumberStyles.Any, null);
        }

        public static float SafeToSingle(string value, float defaultValue, NumberStyles numberStyles)
        {
            return SafeToSingle(value, defaultValue, numberStyles, null);
        }

        public static float SafeToSingle(string value, float defaultValue, NumberStyles numberStyles, Func<float, bool> predicate)
        {
            return SafeToNumber(value, defaultValue, numberStyles, predicate, float.TryParse);
        }

        public static double SafeToDouble(string value)
        {
            return SafeToDouble(value, 0d, NumberStyles.Any, null);
        }

        public static double SafeToDouble(string value, double defaultValue)
        {
            return SafeToDouble(value, defaultValue, NumberStyles.Any, null);
        }

        public static double SafeToDouble(string value, double defaultValue, NumberStyles numberStyles)
        {
            return SafeToDouble(value, defaultValue, numberStyles, null);
        }

        public static double SafeToDouble(string value, double defaultValue, NumberStyles numberStyles, Func<double, bool> predicate)
        {
            return SafeToNumber(value, defaultValue, numberStyles, predicate, double.TryParse);
        }

        public static decimal SafeToDecimal(string value)
        {
            return SafeToDecimal(value, 0, NumberStyles.Any, null);
        }

        public static decimal SafeToDecimal(string value, decimal defaultValue)
        {
            return SafeToDecimal(value, defaultValue, NumberStyles.Any, null);
        }

        public static decimal SafeToDecimal(string value, decimal defaultValue, NumberStyles numberStyles)
        {
            return SafeToDecimal(value, defaultValue, numberStyles, null);
        }

        public static decimal SafeToDecimal(string value, decimal defaultValue, NumberStyles numberStyles, Func<decimal, bool> predicate)
        {
            return SafeToNumber(value, defaultValue, numberStyles, predicate, decimal.TryParse);
        }

        public static bool SafeToBoolean(string value)
        {
            return SafeToBoolean(value, false);
        }

        public static bool SafeToBoolean(string value, bool defaultValue)
        {
            return SafeToBoolean((object)value, defaultValue);
        }

        public static bool SafeToBoolean(object value)
        {
            return SafeToBoolean(value, false);
        }

        public static bool SafeToBoolean(object value, bool defaultValue)
        {
            try
            {
                return ToBoolean(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static bool ToBoolean(object value)
        {
            var newValue = StringUtility.TrimToNull(value);
            bool retval;

            if (newValue == null)
            {
                throw new FormatException("cannot convert an empty string or null string to a Boolean value.");
            }
            else
            {
                newValue = newValue.ToLower();

                if (Array.IndexOf<string>(StringUtility.TrueValues, newValue) > -1)
                    retval = true;
                else if (Array.IndexOf<string>(StringUtility.FalseValues, newValue) > -1)
                    retval = false;
                else
                    throw new FormatException("unable to convert " + newValue + " to a Boolean value.");
            }

            return retval;
        }

        public static bool ToBoolean(string value)
        {
            return ToBoolean((object)value);
        }

        public static TEnum SafeToEnum<TEnum>(string value, TEnum defaultValue)
        {
            return (TEnum)SafeToEnum(typeof(TEnum), value, defaultValue);
        }

        public static object SafeToEnum(Type enumType, string value, object defaultValue)
        {
            var newValue = defaultValue;

            try
            {
                newValue = Enum.Parse(enumType, value, true);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException)
                    throw;

                return defaultValue;
            }

            if (!Enum.IsDefined(enumType, newValue))
                newValue = defaultValue;

            return newValue;
        }


        public static TValue? ToNullable<TValue>(object value) where TValue : struct
        {
            return (TValue?)typeof(TValue?).Create(value);
        }

        public static object ToNullable(object value, Type nullableType)
        {
            return nullableType.Create(value);
        }

        private delegate bool TryParseNumberAction<TValue>(string value, NumberStyles numberStyles, IFormatProvider provider, out TValue parsedValue);

        private static T SafeToNumber<T>(string value, T defaultValue, NumberStyles numberStyles, Func<T, bool> predicate, TryParseNumberAction<T> parseAction)
        {
            T parsedValue;

            if (parseAction(value, numberStyles, null, out parsedValue))
                if (predicate != null)
                    return predicate(parsedValue) ? parsedValue : defaultValue;
                else
                    return parsedValue;
            else
                return defaultValue;
        }

    }
}
