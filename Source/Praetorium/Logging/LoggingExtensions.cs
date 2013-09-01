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
            Log(logger, LogLevel.Debug, message);
        }

        public static void Debug(this ILogger logger, Exception exception)
        {
            logger.Log(LogLevel.Debug, exception);
        }

        public static void Info(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Informational, message, args);
        }

        public static void Info(this ILogger logger, Func<string> message)
        {
            Log(logger, LogLevel.Informational, message);
        }

        public static void Info(this ILogger logger, Exception exception)
        {
            logger.Log(LogLevel.Informational, exception);
        }

        public static void Warn(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Warning, message, args);
        }

        public static void Warn(this ILogger logger, Func<string> message)
        {
            Log(logger, LogLevel.Warning, message);
        }

        public static void Warn(this ILogger logger, Exception exception)
        {
            logger.Log(LogLevel.Warning, exception);
        }

        public static void Error(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Error, message, args);
        }

        public static void Error(this ILogger logger, Func<string> message)
        {
            Log(logger, LogLevel.Error, message);
        }

        public static void Error(this ILogger logger, Exception exception)
        {
            logger.Log(exception);
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
