using System;

namespace Praetorium.Logging
{
    [Serializable]
    public class LogEntry : ILogEntry
    {

        public LogLevel Level
        {
            get;
            set;
        }

        public object EventId
        {
            get;
            set;
        }

        public object Id
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public string Source
        {
            get;
            set;
        }

        public Exception Exception
        {
            get;
            set;
        }

        public object Priority
        {
            get;
            set;
        }

    }
}
