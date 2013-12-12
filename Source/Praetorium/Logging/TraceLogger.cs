using System;
using System.Diagnostics;
using System.IO;

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

            var writer = new StringWriter();

            if (message.IsNotNullOrWhiteSpace())
                writer.WriteLine(message, args);

            if (exception != null)
            {
                var formatter = ExceptionFormatterFactory.Get(exception.GetType());
                formatter.Write(exception, writer);
            }

            _traceSource.TraceEvent(ConvertToTraceEventType(logLevel), _defaultEventId, writer.ToString());
        }

        public override void Log(ILogEntry logEntry)
        {
            Ensure.ArgumentNotNull(() => logEntry);
            Ensure.TypeSupported(() => logEntry, typeof(LogEntry));

            var entry = (LogEntry)logEntry;;
            
            if (IsLoggable(entry.Level))
            {
                var writer = new StringWriter();

                writer.WriteLine("Message: {0}", entry.Message);
                writer.WriteLine("Event Id: {0}", entry.EventId);
                writer.WriteLine("Priority: {0}", entry.Priority);

                if (entry.Exception != null) 
                {
                    var formatter = ExceptionFormatterFactory.Get(entry.Exception.GetType());

                    formatter.Write(entry.Exception, writer);
                }

                var eventId = entry.EventId is int ? (int)entry.EventId : _defaultEventId;

                LogInternal(entry.Level, eventId, writer.ToString());
            }
        }

        protected virtual void LogInternal(LogLevel logLevel, int eventId, string message)
        {
            _traceSource.TraceEvent(ConvertToTraceEventType(logLevel), eventId, message);
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
