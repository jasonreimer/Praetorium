using System;

namespace Praetorium.Eventing
{
    public interface ISubscriptionExpression<TEvent> where TEvent : class
    {
        IDisposable SendTo(Action<TEvent> handler);
    }
}
