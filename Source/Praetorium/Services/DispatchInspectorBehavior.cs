using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Praetorium.Services
{
    public class DispatchInspectorBehavior<TInspector> : IServiceBehavior where TInspector : class, IDispatchMessageInspector
    {
        private readonly TInspector _inspector;

        public DispatchInspectorBehavior(TInspector inspector)
        {
            Ensure.ArgumentNotNull(() => inspector, ref _inspector);
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            Ensure.ArgumentNotNull(() => serviceDescription);
            Ensure.ArgumentNotNull(() => serviceHostBase);

            foreach (var dispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                var channelDispatcher = dispatcherBase as ChannelDispatcher;

                if (channelDispatcher != null)
                    foreach (var endpoint in channelDispatcher.Endpoints)
                        endpoint.DispatchRuntime.MessageInspectors.Add(_inspector);
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }
}
