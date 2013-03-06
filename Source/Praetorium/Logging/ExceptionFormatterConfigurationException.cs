using System;
using System.Runtime.Serialization;

namespace Praetorium.Logging
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public class ExceptionFormatterConfiguraitonException : Exception
    {
        public ExceptionFormatterConfiguraitonException(string message) : base(message) { }
#if !SILVERLIGHT
        protected ExceptionFormatterConfiguraitonException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
#endif
    }
}
