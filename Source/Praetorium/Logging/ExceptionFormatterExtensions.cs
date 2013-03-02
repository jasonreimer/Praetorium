using System;
using System.IO;

namespace Praetorium.Logging
{
    public static class ExceptionFormatterExtensions
    {
        public static string Format(this IExceptionFormatter formatter, Exception exception)
        {
            Ensure.ArgumentNotNull(() => formatter);
            Ensure.ArgumentNotNull(() => exception);

            using (var writer = new StringWriter())
            {
                formatter.Write(exception, writer);

                return writer.ToString();
            }
        }

        public static string Format(this IExceptionFormatterFactory factory, Exception exception)
        {
            Ensure.ArgumentNotNull(() => factory);
            Ensure.ArgumentNotNull(() => exception);

            return factory.Get(exception).Format(exception);
        }
    }
}
