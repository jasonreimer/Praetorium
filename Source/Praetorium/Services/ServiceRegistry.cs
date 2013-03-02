using System.ServiceModel;
using System.ServiceModel.Description;

namespace Praetorium.Services
{

    /// <summary>
    /// A WCF implementation of the <see cref="IServiceRegistry"/> interface, which 
    /// provide the capability to create WCF channels to endpoints using the .NET 
    /// Configuration system.
    /// </summary>
    public class ServiceRegistry : IRemoteServiceRegistry
    {
        private readonly IEndpointBehavior[] _endpointBehaviors;

        public ServiceRegistry(IEndpointBehavior[] endpointBehaviors)
        {
            _endpointBehaviors = endpointBehaviors ?? new IEndpointBehavior[0];
        }

        public ServiceRegistry()
            : this(null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public TService GetProxy<TService>() where TService : class
        {
            var serviceConfigurationName = GetServiceConfigurationName<TService>();

            if (serviceConfigurationName.IsNullOrEmpty())
                throw Errors.ChannelFactoryWithNoEndpointException(typeof(TService).FullName);

            return GetProxy<TService>(serviceConfigurationName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceConfigurationName"></param>
        /// <returns></returns>
        public TService GetProxy<TService>(string serviceConfigurationName) where TService : class
        {
            Ensure.ArgumentNotNullOrWhiteSpace(() => serviceConfigurationName);

            var properties = new ServiceLocationProperties { ServiceConfigurationName = serviceConfigurationName };
            var channelFactory = CreateChannelFactory<TService>(properties);

            return channelFactory.CreateChannel();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="locationProperties"></param>
        /// <returns>
        /// </returns>
        public TService GetProxy<TService>(ServiceLocationProperties locationProperties) where TService : class
        {
            Ensure.ArgumentNotNull(() => locationProperties);

            var channelFactory = CreateChannelFactory<TService>(locationProperties);

            return channelFactory.CreateChannel();
        }

        protected virtual ChannelFactory<TService> CreateChannelFactory<TService>(ServiceLocationProperties properties)
        {
            Ensure.ArgumentNotNull(() => properties);

            EndpointAddress address = null;
            ChannelFactory<TService> channelFactory = null;

            if (properties.ServiceConfigurationName.IsNullOrEmpty())
                throw Errors.ServiceConfigurationNamePropertyIsRequired();

            if (properties.ServiceLocation.IsNotNullOrWhiteSpace())
                address = new EndpointAddress(properties.ServiceLocation);

            channelFactory = new ChannelFactory<TService>(properties.ServiceConfigurationName, address);

            if (properties.UserName.IsNotNullOrWhiteSpace())
            {
                channelFactory.Credentials.UserName.UserName = properties.UserName;
                channelFactory.Credentials.UserName.Password = properties.Password;
            }

            _endpointBehaviors.ForEach(channelFactory.Endpoint.Behaviors.Add);

            return channelFactory;
        }

        protected virtual string GetServiceConfigurationName<TService>() where TService : class
        {
            string name = null;
            var serviceType = typeof(TService);
            var defaultEndpointAttrib = serviceType.GetAttribute<DefaultEnpointNameAttribute>(true);

            if (defaultEndpointAttrib != null)
            {
                name = defaultEndpointAttrib.Name;
            }
            else
            {
                var serviceContractAttrib = serviceType.GetAttribute<ServiceContractAttribute>();

                if (serviceContractAttrib != null)
                    name = serviceContractAttrib.Name;
            }

            return name;
        }
    }
}
