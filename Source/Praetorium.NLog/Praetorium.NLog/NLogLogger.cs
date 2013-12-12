using NLog;
using Praetorium.Logging;
using System;
using System.IO;
using LogLevel = Praetorium.Logging.LogLevel;
using NLogLevel = NLog.LogLevel;

namespace Praetorium.NLog
{
    public class NLogLogger : LoggerBase
    {
        private readonly Logger _logger;

        public NLogLogger(IExceptionFormatterFactory exceptionFormatterFactory)
            : this(LogManager.GetLogger("Root"), exceptionFormatterFactory)
        {
        }

        protected internal NLogLogger(Logger logger, IExceptionFormatterFactory exceptionFormatterFactory)
            : base(exceptionFormatterFactory)
        {
            Ensure.ArgumentNotNull(() => logger, ref _logger);
        }

        public override bool IsLoggable(LogLevel logLevel)
        {
            return _logger.IsEnabled(ConvertTo(logLevel));
        }

        public override void Log(ILogEntry logEntry)
        {
            Ensure.ArgumentNotNull(() => logEntry);
            Ensure.TypeSupported(() => logEntry, typeof(LogEntry));

            var entry = (LogEntry)logEntry;
            var nlogLevel = ConvertTo(entry.Level);

            Log(entry.Level, entry.Exception, entry.Message);
        }

        public override void Log(LogLevel logLevel, Exception exception, string message, params object[] args)
        {
            var nlogLevel = ConvertTo(logLevel);

            if ((exception == null && message.IsNullOrWhiteSpace()) || !IsLoggable(logLevel))
                return;

            var writer = new StringWriter();

            if (message.IsNotNullOrWhiteSpace())
                writer.WriteLine(message, args);

            if (exception != null)
            {
                var formatter = ExceptionFormatterFactory.Get(exception.GetType());

                formatter.Write(exception, writer);
            }

            var logEventInfo = LogEventInfo.Create(nlogLevel, _logger.Name, writer.ToString(), exception);

            Log(logEventInfo);
        }

        protected virtual void Log(LogEventInfo logEventInfo)
        {
            _logger.Log(logEventInfo);
        }

        private static NLogLevel ConvertTo(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    return NLogLevel.Debug;
                case LogLevel.Error:
                    return NLogLevel.Error;
                case LogLevel.Fatal:
                    return NLogLevel.Fatal;
                case LogLevel.Informational:
                    return NLogLevel.Info;
                case LogLevel.Trace:
                    return NLogLevel.Trace;
                case LogLevel.Warning:
                    return NLogLevel.Warn;
            }

            Ensure.EnumValueIsDefined(() => logLevel);

            return NLogLevel.Trace;
        }
    }
}
