namespace Praetorium.Eventing
{
    public static class EventAggregatorExtensions
    {
        public static IEventAggregator Send<TEvent>(this IEventAggregator eventAggregator) where TEvent : class, new()
        {
            return eventAggregator.Send(new TEvent());
        }

        public static IEventAggregator RegisterListeners(this IEventAggregator eventAggregator, params IListener[] listeners)
        {
            listeners.ForEach(l => eventAggregator.RegisterListener(l));

            return eventAggregator;
        }

        public static void SafeHandle<TEvent>(this IListener<TEvent> listener, TEvent @event) where TEvent : class
        {
            try
            {
                listener.Handle(@event);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Creates a subscription to the specified <typeparamref name="TEvent"/>.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="expression">The expression used to configure the event subscription.</param>
        public static ISubscriptionExpression<TEvent> SubscribeTo<TEvent>(this IEventAggregator eventAggregator) where TEvent : class
        {
            return new SubscriptionExpression<TEvent>(eventAggregator);
        }

#if NET45
        public static async void SendAsync<TEvent>(this IEventAggregator eventAggregator, TEvent @event) where TEvent : class
        {
            eventAggregator.Send(@event);
        }
#endif
    }
}
