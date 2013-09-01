using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using Praetorium.Eventing;

namespace Praetorium.UnitTests.Eventing
{
    [Subject(Subjects.EventAggregation)]
    class When_a_listener_interface_is_injected
    {
        protected static IListener Listener;
        protected static EventAggregator Aggregator;

        Establish context = () =>
        {
            Listener = MockRepository.GenerateStrictMock<IListener<NulloEvent>>();
            Aggregator = new EventAggregator(new[] { Listener });
        };

        Because of = () => Aggregator.Send<NulloEvent>();

        Behaves_like<MessageReceivedBehavior<NulloEvent>> a_nullo_listener;
    }
}
