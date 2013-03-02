using System;

namespace Praetorium.Eventing
{
    public interface ISubscriptionExpression<TEvent> where TEvent : class
    {
        IEventAggregator SendTo(Action<TEvent> handler);
    }
}
