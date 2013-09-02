using System;

namespace Praetorium.Logging
{
    public class NulloLogger : LoggerBase
    {
        public override bool IsLoggable(LogLevel logLevel)
        {
            return false;
        }

        public override void Log(LogLevel logLevel, Exception exception, string message, params object[] args) {}

        public override void Log(ILogEntry logEntry) { }
    }
}
