using Machine.Specifications;
using Praetorium.Eventing;

namespace Praetorium.UnitTests.Eventing
{
    [Subject(Subjects.EventAggregation)]
    class When_a_dual_listener_is_registered : DualListenerInterfaceContext
    {
        Because of = () =>
            aggregator.Send<NulloEvent>()
                      .Send("test");

        Behaves_like<MessageReceivedBehavior<NulloEvent>> a_nullo_listener;
        
        Behaves_like<MessageReceivedBehavior<NulloEvent>> a_string_listener;
    }
}
