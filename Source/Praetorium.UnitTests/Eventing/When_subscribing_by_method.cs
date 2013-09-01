using Machine.Specifications;
using Praetorium.Eventing;

namespace Praetorium.UnitTests.Eventing
{
    class When_subscribing_by_method
    {
        static SampleListener Listener;
        static EventAggregator Aggregator;

        Establish context = () =>
        {
            Listener = new SampleListener();
            Aggregator = new EventAggregator();
        };

        Because of = () =>
        {
            Aggregator.SubscribeTo<NulloEvent>()
                      .SendTo(Listener.Handle);
            Aggregator.Send<NulloEvent>();
        };

        It should_receive_the_message_on_send = () =>
            Listener.CallCount.ShouldEqual(1);
    }

    class SampleListener
    {
        public int CallCount { get; set; }

        public void Handle(NulloEvent e)
        {
            CallCount++;
        }
    }
}
