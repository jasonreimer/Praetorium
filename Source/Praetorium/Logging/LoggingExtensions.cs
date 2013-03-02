using System;

namespace Praetorium.Logging
{
    public static class LoggingExtensions
    {
        public static void Debug(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Debug, message, args);
        }

        public static void Debug(this ILogger logger, Func<string> message)
        {
            Log(logger, LogLevel.Error, message);
        }

        public static void Error(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Error, message, args);
        }

        public static void Error(this ILogger logger, Func<string> message)
        {
            Log(logger, LogLevel.Error, message);
        }

        private static void Log(ILogger logger, LogLevel logLevel, Func<string> message)
        {
            if (logger.IsLoggable(logLevel))
            {
                logger.Log(logLevel, message());
            }
        }
    }
}
