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

        public IEventAggregator SendTo(Action<TEvent> handler)
        {
            _eventAggregator.RegisterListener(new SubscriptionListener<TEvent>(handler));

            return _eventAggregator;
        }
    }
}
