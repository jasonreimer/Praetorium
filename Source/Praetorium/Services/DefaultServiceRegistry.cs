using System.ServiceModel;
using System.ServiceModel.Description;

namespace Praetorium.Services
{
    public class DefaultServiceRegistry : ServiceRegistry
    {
        private readonly IEndpointBehavior[] _endpointBehaviors;

        public DefaultServiceRegistry(IEndpointBehavior[] endpointBehaviors)
        {
            _endpointBehaviors = endpointBehaviors ?? new IEndpointBehavior[0];
        }

        protected override ChannelFactory<TService> CreateChannelFactory<TService>(ServiceLocationProperties properties)
        {
            var channelFactory = base.CreateChannelFactory<TService>(properties);

            _endpointBehaviors.ForEach(channelFactory.Endpoint.Behaviors.Add);

            return channelFactory;
        }
    }
}
