using NLog;
using Praetorium.Logging;

namespace Praetorium.NLog.Tests
{
    public class TestableNLogLogger : NLogLogger
    {
        public TestableNLogLogger(IExceptionFormatterFactory exceptionFormatterFactory)
            : base(exceptionFormatterFactory)
        {
        }

        public LogEventInfo EventLogged { get; set; }
        public bool Enabled { get; set; }

        public override bool IsLoggable(Logging.LogLevel logLevel)
        {
            return Enabled;
        }

        protected override void Log(LogEventInfo logEventInfo)
        {
            EventLogged = logEventInfo;
        }
    }
}
