using System;

namespace Praetorium.Services
{
    /// <summary>
    /// A WCF implementation of the <see cref="IServiceRegistry"/> interface, which 
    /// provide the capability to create WCF channels to endpoints using the .NET 
    /// Configuration system.
    /// </summary>
    public sealed class CachedChannelServiceRegistry : IRemoteServiceRegistry
    {
        public TService GetProxy<TService>() where TService : class
        {
            var serviceType = GetServiceType<TService>();

            return ServiceTypeCache<TService>.Instance.CreateChannel();
        }

        public TService GetProxy<TService>(string serviceConfigurationName) where TService : class
        {
            Ensure.ArgumentNotNullOrWhiteSpace(() => serviceConfigurationName);

            var serviceType = GetServiceType<TService>();

            return serviceType.CreateChannel(serviceConfigurationName);
        }

        public TService GetProxy<TService>(ServiceLocationProperties locationProperties) where TService : class
        {
            Ensure.ArgumentNotNull(() => locationProperties);

            var serviceType = GetServiceType<TService>();

            return serviceType.CreateChannel(locationProperties);
        }

        private ServiceType<TService> GetServiceType<TService>() where TService : class
        {
            try
            {
                return ServiceTypeCache<TService>.Instance;
            }
            catch (TypeInitializationException exception)
            {
                throw Errors.ServiceTypeCacheUnexpectedException(typeof(TService), exception);
            }
        }
    }
}
