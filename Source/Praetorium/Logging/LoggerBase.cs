using System;

namespace Praetorium.Logging
{
    public abstract class LoggerBase : ILogger
    {
        public abstract bool IsLoggable(LogLevel logLevel);

        public LoggerBase(IExceptionFormatterFactory exceptionFormatterFactory)
        {
            Ensure.ArgumentNotNull(() => exceptionFormatterFactory);

            ExceptionFormatterFactory = exceptionFormatterFactory;
        }

        protected LoggerBase()
        {
        }

        protected IExceptionFormatterFactory ExceptionFormatterFactory
        {
            get;
            private set;
        }

        public void Log(LogLevel logLevel, string message, params object[] args)
        {
            Log(logLevel, null, message, args);
        }

        public void Log(Exception exception)
        {
            Log(LogLevel.Error, exception, (string)null);
        }

        public void Log(LogLevel logLevel, Exception exception)
        {
            Log(logLevel, exception, (string)null);
        }

        public abstract void Log(LogLevel logLevel, Exception exception, string message, params object[] args);

        public abstract void Log(ILogEntry logEntry);
    }
}
