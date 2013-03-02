using System;
using System.Runtime.Serialization;

namespace Praetorium.Services
{
    [Serializable]
    public class ServiceTypeCacheException : Exception
    {
        public ServiceTypeCacheException() { }
        public ServiceTypeCacheException(string message) : base(message) { }
        public ServiceTypeCacheException(string message, Exception inner) : base(message, inner) { }
        protected ServiceTypeCacheException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
