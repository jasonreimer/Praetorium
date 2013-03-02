using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Praetorium.Services
{
    public class ServiceFactoryBehavior : IServiceBehavior
    {
        private readonly Func<Type, object> _typeFactory;

        public ServiceFactoryBehavior(Func<Type, object> typeFactory)
        {
            Ensure.ArgumentNotNull(() => typeFactory, ref _typeFactory);
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            Ensure.ArgumentNotNull(() => serviceHostBase);
            Ensure.ArgumentNotNull(() => serviceDescription);

            foreach (ChannelDispatcherBase dispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                var dispatcher = dispatcherBase as ChannelDispatcher;

                if (dispatcher != null)
                    foreach (var endpoint in dispatcher.Endpoints)
                        endpoint.DispatchRuntime.InstanceProvider = new BasicInstanceProvider(() => _typeFactory(serviceDescription.ServiceType));
            }
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }

}
