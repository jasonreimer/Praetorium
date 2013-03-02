using System;
using System.Runtime.Serialization;

namespace Praetorium.Logging
{
    [Serializable]
    public class ExceptionFormatterConfiguraitonException : Exception
    {
        public ExceptionFormatterConfiguraitonException(string message) : base(message) { }
        protected ExceptionFormatterConfiguraitonException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
