using Machine.Specifications;
using Praetorium.Eventing;
using System;

namespace Praetorium.UnitTests.Eventing
{
    class ActionListenerContext
    {
        protected static IEventAggregator Aggregator;
        protected static IDisposable Listener;
        protected static int CallCount;

        Establish context = () =>
        {
            Aggregator = new EventAggregator();
            Listener = Aggregator.SubscribeTo<NulloEvent>()
                                 .SendTo(e => CallCount++);
        };
    }
}
