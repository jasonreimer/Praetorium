using NUnit.Framework;
using Praetorium.Eventing;
using Should;
using System;

namespace Praetorium.Tests.Eventing
{

    [TestFixture(Category = Categories.Eventing)]
    public class Listener_subscriptions
    {
        [Test]
        public void should_receive_message_on_send()
        {
            var listener = new EmptyEventListener();
            var eventAggregator = new EventAggregator();

            eventAggregator
                .RegisterListener(listener)
                .Send<EmptyEvent>();

            listener.EmptyHandleCount.ShouldEqual(1);
        }

        [Test]
        public void can_listen_for_many_message_types()
        {
            var listener = new EmptyEventListener();
            var eventAggregator = new EventAggregator();

            eventAggregator
                .RegisterListener(listener)
                .Send("test");

            listener.StringHandleCount.ShouldEqual(1);
        }

        [Test]
        public void should_not_receive_messages_after_disposal()
        {
            var listener = new EmptyEventListener();
            var eventAggregator = new EventAggregator();

            eventAggregator
                .RegisterListener(listener)
                .UnregisterListener(listener)
                .Send("test");

            listener.StringHandleCount.ShouldEqual(0);
        }
    }

    [TestFixture(Category = Categories.Eventing)]
    public class Injected_listeners
    {
        [Test]
        public void should_receive_messages()
        {
            var listener = new EmptyEventListener();
            var eventAggregator = new EventAggregator(new IListener[] { listener });

            eventAggregator.Send<EmptyEvent>();

            listener.EmptyHandleCount.ShouldEqual(1);
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
            var eventAggregator = new EventAggregator();
            
            eventAggregator.SubscribeTo<EmptyEvent>().SendTo(e => number++);
            eventAggregator.Send<EmptyEvent>();

            number.ShouldEqual(1);
        }
    }

    public class EmptyEvent { }

    public class EmptyEventListener : IListener<EmptyEvent>, IListener<string>
    {
        public int EmptyHandleCount { get; private set; }
        public int StringHandleCount { get; private set; }

        public void Handle(EmptyEvent message)
        {
            EmptyHandleCount++;
        }

        public void Handle(string message)
        {
            StringHandleCount++;
        }
    }

    public class ActionEventListener<TEvent> : IListener<TEvent> where TEvent : class
    {
        private readonly Action<TEvent> _action;

        public ActionEventListener(Action<TEvent> action)
        {
            _action = action;
        }

        public void Handle(TEvent message)
        {
            _action(message);
        }
    }
}
