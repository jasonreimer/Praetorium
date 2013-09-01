using Machine.Specifications;
using Praetorium.Eventing;
using Rhino.Mocks;

namespace Praetorium.UnitTests.Eventing
{
    class DualListenerInterfaceContext
    {
        protected static object Listener;
        protected static IEventAggregator aggregator;

        Establish context = () =>
        {
            Listener = MockRepository.GenerateStrictMock<IListener<NulloEvent>, IListener<string>>();
            aggregator = new EventAggregator().RegisterListener(Listener);
        };
    }
}
