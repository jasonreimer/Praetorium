using NLog;
using Praetorium.Logging;
using System;
using System.Text;
using LogLevel = Praetorium.Logging.LogLevel;
using NLogLevel = NLog.LogLevel;

namespace Praetorium.NLog
{
    public class NLogLogger : LoggerBase
    {
        private readonly IExceptionFormatterFactory _exceptionFormatterFactory;
        private readonly Logger _logger;

        public NLogLogger(IExceptionFormatterFactory exceptionFormatterFactory)
            : this(LogManager.GetLogger("Root"), exceptionFormatterFactory)
        {
        }

        internal NLogLogger(Logger logger, IExceptionFormatterFactory exceptionFormatterFactory)
        {
            Ensure.ArgumentNotNull(() => logger, ref _logger);
            Ensure.ArgumentNotNull(() => exceptionFormatterFactory, ref _exceptionFormatterFactory);
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

            if (_logger.IsEnabled(nlogLevel))
            {
                var messageBuilder = new StringBuilder()
                    .AppendFormat("Message: {0}", entry.Message).AppendLine();

                if (entry.Exception != null) 
                    messageBuilder.Append(ExceptionFormatterFactory.Format(entry.Exception));

                var logEventInfo = LogEventInfo.Create(nlogLevel, _logger.Name, messageBuilder.ToString(), entry.Exception);

                _logger.Log(logEventInfo);
            }
        }

        public override void Log(LogLevel logLevel, Exception exception, string message, params object[] args)
        {
            var nlogLevel = ConvertTo(logLevel);

            if ((exception == null && message.IsNullOrWhiteSpace()) || !_logger.IsEnabled(nlogLevel))
                return;

            var messageBuilder = new StringBuilder();

            if (message.IsNotNullOrWhiteSpace())
            {
                messageBuilder.AppendFormat(message, args);
                messageBuilder.AppendLine();
            }

            if (exception != null)
                messageBuilder.AppendLine(ExceptionFormatterFactory.Format(exception));

            var logEventInfo = LogEventInfo.Create(nlogLevel, _logger.Name, messageBuilder.ToString(), exception);

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
