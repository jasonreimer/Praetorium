using Machine.Specifications;
using Praetorium.Eventing;

namespace Praetorium.UnitTests.Eventing
{
    [Subject(Subjects.EventAggregation)]
    class When_a_dual_listener_is_unregistered : DualListenerInterfaceContext
    {
        Because of = () =>
            aggregator.UnregisterListener(Listener)
                      .Send<NulloEvent>()
                      .Send("test");

        Behaves_like<MessageNotReceivedBehavior<NulloEvent>> a_unregistered_nullo_listener;
        
        Behaves_like<MessageNotReceivedBehavior<string>> a_unregistered_string_listener;
    }
}
