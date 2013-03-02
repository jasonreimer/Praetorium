namespace Praetorium
{
    public static class PrimitiveExtensions
    {
        public static string ToFormat<T>(this T value, string format) where T : struct
        {
            return string.Format(format, value);
        }

        public static string ToHex(this int value)
        {
            return value.ToString("X");
        }

        public static string ToHex(this int value, int precision)
        {
            return value.ToString("X" + precision.ToString());
        }
    }
}
