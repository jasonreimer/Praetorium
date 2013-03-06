using System;
#if !SILVERLIGHT
using System.Data.SqlTypes;
#endif

namespace Praetorium
{
    public static class DateTimeExtensions
    {
        public static int MillisecondsTillNow(this DateTime instance)
        {
            return instance.DurationTillNow().Milliseconds;
        }

        public static TimeSpan DurationTillNow(this DateTime instance)
        {
            return DateTime.Now - instance;
        }

        public static int MillisecondsBetween(this DateTime instance, DateTime value)
        {
            return Math.Abs((value - instance).Milliseconds);
        }

        public static int SecondsBetween(this DateTime instance, DateTime value)
        {
            return Math.Abs((value - instance).Seconds);
        }

        public static int MinutesBetween(this DateTime instance, DateTime value)
        {
            return Math.Abs((value - instance).Minutes);
        }

        public static int HoursBetween(this DateTime instance, DateTime value)
        {
            return Math.Abs((value - instance).Hours);
        }

        public static int DaysBetween(this DateTime instance, DateTime value)
        {
            return Math.Abs((value - instance).Days);
        }

        /// <summary>
        /// Calculates the years between the <paramref name="instance"/> and the 
        /// <paramref name="value"/>.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The difference in years.
        /// </returns>
        public static int YearsBetween(this DateTime instance, DateTime value)
        {

            // get the difference in years
            int years = instance.Year - value.Year;

            // subtract another year if we're before the day in the current year
            if (instance.Month < value.Month
               || (instance.Month == value.Month && instance.Day < value.Day))
                --years;

            return years;
        }

        public static DateTime? ReplaceMinWithNull(this DateTime? value)
        {
            return value != null && value.HasValue && value.Value == DateTime.MinValue 
                        ? null : value;
        }

        public static DateTime? ReplaceNullWithMin(this DateTime? value)
        {
            return value == null || !value.HasValue ? DateTime.MinValue : value;
        }

#if !SILVERLIGHT

        public static DateTime? ReplaceNullWithSqlMin(this DateTime? value)
        {
            return value == null || !value.HasValue ? SqlDateTime.MinValue.Value : value;
        }

        public static DateTime? ReplaceMinOrNullWithSqlMin(this DateTime? value)
        {
            return value == null || !value.HasValue || (value.HasValue && value.Value == DateTime.MinValue) 
                ? SqlDateTime.MinValue.Value : value;
        }

        public static DateTime ReplaceMinWithSqlMin(this DateTime value)
        {
            return value == DateTime.MinValue ? SqlDateTime.MinValue.Value : value;
        }

#endif

    }
}
