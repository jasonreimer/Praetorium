using Machine.Specifications;
using Machine.Specifications.Model;
using Praetorium.Eventing;

namespace Praetorium.UnitTests.Eventing
{
    [Subject(Subjects.EventAggregation)]
    class When_a_listener_interface_is_unregistered : DualListenerInterfaceContext
    {
        Because of = () =>
            aggregator.UnregisterListener(Listener)
                      .Send<NulloEvent>();

        Behaves_like<MessageNotReceivedBehavior<NulloEvent>> a_unregistered_listener;
    }
}
