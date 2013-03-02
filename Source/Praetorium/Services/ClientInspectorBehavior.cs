using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Praetorium.Services
{
    public class ClientInspectorBehavior<TInspector> : IEndpointBehavior where TInspector : class, IClientMessageInspector
    {
        private readonly TInspector _inspector;

        public ClientInspectorBehavior(TInspector inspector)
        {
            Ensure.ArgumentNotNull(() => inspector, ref _inspector);
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            Ensure.ArgumentNotNull(() => clientRuntime);

            clientRuntime.MessageInspectors.Add(_inspector);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
