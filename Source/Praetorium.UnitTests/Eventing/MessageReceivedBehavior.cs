using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;

namespace Praetorium.UnitTests.Eventing
{
    [Behaviors]
    class MessageReceivedBehavior<TEvent> where TEvent : class
    {
        protected static object Listener = null;

        It should_receive_the_expected_message = () =>
            Listener.As<IListener<TEvent>>().AssertWasCalled(x => x.Handle(null), o => o.IgnoreArguments().Constraints(Is.NotNull()));
    }
}
