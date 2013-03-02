using System;

namespace Praetorium.Eventing
{
    public class SubscriptionExpression<TEvent> : ISubscriptionExpression<TEvent> where TEvent : class
    {
        private readonly IEventAggregator _eventAggregator;

        public SubscriptionExpression(IEventAggregator eventAggregator)
        {
            Ensure.ArgumentNotNull(() => eventAggregator, ref _eventAggregator);
        }

        public IDisposable SendTo(Action<TEvent> handler)
        {
            var listener = new SubscriptionListener<TEvent>(handler);

            _eventAggregator.RegisterListener(listener);

            return new ActionDisposable(() => _eventAggregator.UnregisterListener(listener));
        }
    }
}
