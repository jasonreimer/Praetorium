using Machine.Specifications;
using Praetorium.Eventing;

namespace Praetorium.UnitTests.Eventing
{
    [Subject(Subjects.EventAggregation)]
    class When_subscribing_by_action : ActionListenerContext
    {
        Because of = () => Aggregator.Send<NulloEvent>();

        It should_call_the_action_on_send = () =>
            CallCount.ShouldEqual(1);
    }
}
