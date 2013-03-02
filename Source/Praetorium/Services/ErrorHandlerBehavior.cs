using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Praetorium.Services
{
    public class ErrorHandlerBehavior : IServiceBehavior
    {
        private readonly IErrorHandler[] _errorHandlers;

        public ErrorHandlerBehavior(IErrorHandler[] errorHandlers)
        {
            _errorHandlers = errorHandlers ?? new IErrorHandler[0];
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            Ensure.ArgumentNotNull(() => serviceHostBase);

            foreach (var dispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                var channelDispatcher = dispatcherBase as ChannelDispatcher;

                if (channelDispatcher != null)
                    _errorHandlers.ForEach(channelDispatcher.ErrorHandlers.Add);
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }
}
