using System;
using System.Collections.Generic;
using System.Linq;

namespace Praetorium.Eventing
{
    public class EventAggregator : IEventAggregator
    {
        private readonly object _locker = new object();
        private List<IEventSubscription> _subscriptions = new List<IEventSubscription>();

        public EventAggregator(IListener[] listeners)
        {
            listeners = listeners ?? new IListener[0];
            this.RegisterListeners(listeners);
        }

        public EventAggregator()
            : this(null)
        {
        }

        public virtual IEventAggregator Send<TEvent>(TEvent @event) where TEvent : class
        {
            Ensure.ArgumentNotNull(() => @event);

            SendToEach(@event, GetListeners<TEvent>());

            return this;
        }

        public IEventAggregator RegisterListener(object listener)
        {
            if (listener == null) return this;

            IEventSubscription subscription;

            if (listener is IEventSubscription)
                subscription = (IEventSubscription)listener;
            else 
                subscription = new WeakSubscription(listener);

            AddSubscription(subscription);

            return this;
        }

        public IEventAggregator UnregisterListener(object listener)
        {
            if (listener == null) return this;

            RemoveSubscriptions(x => x.RelatesTo(listener));

            return this;
        }

        protected IListener<TEvent>[] GetListeners<TEvent>() where TEvent : class
        {
            lock (_locker)
            {
                _subscriptions.RemoveAll(sub => sub.IsDead);

                return _subscriptions
                            .Where(s => s.ListenerType.Is<IListener<TEvent>>())
                            .Select(s => s.GetListener<TEvent>())
                            .ToArray();
            }
        }

        protected virtual void SendToEach<TEvent>(TEvent @event, IEnumerable<IListener<TEvent>> listeners) where TEvent : class
        {
            listeners.ForEach(l => l.SafeHandle(@event));
        }

        private void AddSubscription(IEventSubscription subscription)
        {
            Ensure.ArgumentNotNull(() => subscription);

            lock (_locker)
            {
                _subscriptions.Add(subscription);
            }
        }

        private void RemoveSubscriptions(Predicate<IEventSubscription> predicate)
        {
            Ensure.ArgumentNotNull(() => predicate);

            lock (_locker)
            {
                _subscriptions.RemoveAll(predicate);
            }
        }
    }
}
