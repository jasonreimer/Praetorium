using Machine.Specifications;
using Praetorium.Eventing;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;

namespace Praetorium.UnitTests.Eventing
{
    [Subject(Subjects.EventAggregation)]
    class When_a_listener_interface_is_registered : DualListenerInterfaceContext
    {
        Because of = () => aggregator.Send<NulloEvent>();

        Behaves_like<MessageReceivedBehavior<NulloEvent>> a_nullo_listener;
    }
}
