using NUnit.Framework;
using Praetorium.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Should;

namespace Praetorium.Tests.Eventing
{
    [TestFixture(Category = Categories.Eventing)]
    public class Listener_subscriptions
    {
        [Test]
        public void via_injetion_should_receive_messages()
        {
            Assert.Fail();
        }

        [Test]
        public void should_receive_message_on_send()
        {
            Assert.Fail();
        }

        [Test]
        public void can_listen_for_many_message_types()
        {
            Assert.Fail();
        }

        [Test]
        public void should_not_receive_messages_after_disposal()
        {
            Assert.Fail();
        }
    }

    [TestFixture(Category = Categories.Eventing)]
    public class Disposed_action_subscriptions
    {
        [Test]
        public void should_not_receive_messages()
        {
            int number = 0;

            var eventAggregator = new EventAggregator(null);
            eventAggregator.SubscribeTo<EmptyEvent>().SendTo(e => number++).Dispose();
            eventAggregator.Send<EmptyEvent>();

            number.ShouldEqual(0);
        }
    }

    [TestFixture(Category=Categories.Eventing)]
    public class Action_subscriptions
    {
        [Test]
        public void should_receive_message_on_send()
        {
            int number = 0;
            var eventAggregator = new EventAggregator(null);
            
            eventAggregator.SubscribeTo<EmptyEvent>().SendTo(e => number++);
            eventAggregator.Send<EmptyEvent>();

            number.ShouldEqual(1);
        }
    }

    public class EmptyEvent { }
}
