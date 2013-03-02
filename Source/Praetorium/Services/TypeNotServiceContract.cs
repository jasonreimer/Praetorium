using System;

namespace Praetorium.Services
{
    [Serializable]
    public class TypeNotServiceContractException : Exception
    {
        public TypeNotServiceContractException() { }
        public TypeNotServiceContractException(string message) : base(message) { }
        public TypeNotServiceContractException(string message, Exception inner) : base(message, inner) { }
        protected TypeNotServiceContractException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
