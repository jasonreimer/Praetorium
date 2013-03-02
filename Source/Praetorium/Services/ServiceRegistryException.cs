using System;
using System.Runtime.Serialization;

namespace Praetorium.Services
{
    public class ServiceNotLocatedExceptionDataNames
    {
        public const string ServiceName = "ServiceName";
    }

    [Serializable]
    public class ServiceRegistryException : Exception
    {

        public ServiceRegistryException(string message)
            : base(message)
        {
        }

        public ServiceRegistryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ServiceRegistryException(string message, Exception innerException, ServiceLocationProperties locationProperties)
            : base(message, innerException)
        {
            AddServiceLocationProperties(locationProperties);
        }

        public ServiceRegistryException(ServiceLocationProperties locationProperties)
            : base(locationProperties.ServiceName + " could not be located.")
        {
            AddServiceLocationProperties(locationProperties);
        }

        protected ServiceRegistryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private void AddServiceLocationProperties(ServiceLocationProperties locationProperties)
        {
            Data.Add(ServiceNotLocatedExceptionDataNames.ServiceName, locationProperties.ServiceName);
        }

    }
}
