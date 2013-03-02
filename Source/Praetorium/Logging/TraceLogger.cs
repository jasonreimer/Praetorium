using System;
using System.Diagnostics;
using System.Text;

namespace Praetorium.Logging
{
    public class TraceLogger : LoggerBase
    {

        private const int _defaultEventId = 0;

        private readonly TraceSource _traceSource;

        public TraceLogger(IExceptionFormatterFactory exceptionFormatterFactory)
            : this(new TraceSource("Root"), exceptionFormatterFactory)
        {
        }

        internal TraceLogger(TraceSource traceSource, IExceptionFormatterFactory exceptionFormatterFactory)
            : base(exceptionFormatterFactory)
        {
            Ensure.ArgumentNotNull(() => traceSource, ref _traceSource);
        }

        public override bool IsLoggable(LogLevel logLevel)
        {
            return _traceSource.Switch.ShouldTrace(ConvertToTraceEventType(logLevel));
        }

        public override void Log(LogLevel logLevel, Exception exception, string message, params object[] args)
        {
            if ((exception == null && message.IsNullOrWhiteSpace()) || !IsLoggable(logLevel))
                return;

            var messageBuilder = new StringBuilder();

            if (message.IsNotNullOrWhiteSpace()) 
            {
                messageBuilder.AppendFormat(message, args);
                messageBuilder.AppendLine();
            }

            if (exception != null)
                messageBuilder.AppendLine(ExceptionFormatterFactory.Format(exception));

            _traceSource.TraceEvent(TraceEventType.Error, _defaultEventId, messageBuilder.ToString());
        }

        public override void Log(ILogEntry logEntry)
        {
            Ensure.ArgumentNotNull(() => logEntry);
            Ensure.TypeSupported(() => logEntry, typeof(LogEntry));

            var entry = (LogEntry)logEntry;
            var eventType = ConvertToTraceEventType(entry.Level);

            if (_traceSource.Switch.ShouldTrace(eventType))
            {
                var messageBuilder = new StringBuilder()
                    .AppendFormat("Message: {0}", entry.Message).AppendLine()
                    .AppendFormat("Event Id: {0}", entry.EventId).AppendLine()
                    .AppendFormat("Priority: {0}", entry.Priority).AppendLine()
                    .AppendIf(entry.Exception != null, ExceptionFormatterFactory.Format(entry.Exception));

                _traceSource.TraceEvent(eventType, _defaultEventId, messageBuilder.ToString());
            }
        }

        private static TraceEventType ConvertToTraceEventType(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                    return TraceEventType.Verbose;
                case LogLevel.Informational:
                    return TraceEventType.Information;
                case LogLevel.Warning:
                    return TraceEventType.Warning;
                case LogLevel.Error:
                    return TraceEventType.Error;
                case LogLevel.Fatal:
                    return TraceEventType.Critical;
            }

            Ensure.EnumValueIsDefined(() => logLevel);

            // we shouldn't get this far.
            return TraceEventType.Verbose;
        }

    }
}
