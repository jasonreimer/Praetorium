using System;

namespace Praetorium.Logging
{
    [Serializable]
    public enum LogLevel
    {
        Trace,
        Debug,
        Informational,
        Warning,
        Error,
        Fatal
    }
}
