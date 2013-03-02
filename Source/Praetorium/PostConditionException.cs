using System;
using System.Runtime.Serialization;

namespace Praetorium
{
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public class PostConditionException : Exception
    {

        public PostConditionException() { }
        public PostConditionException(string message) : base(message) { }
        public PostConditionException(string message, Exception inner) : base(message, inner) { }

#if !SILVERLIGHT

        protected PostConditionException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

#endif

    }
}
