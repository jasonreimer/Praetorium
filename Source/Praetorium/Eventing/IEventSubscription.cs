using System;

namespace Praetorium.Eventing
{
    internal interface IEventSubscription
    {
        Type ListenerType { get; }
        bool IsDead { get; }
        bool RelatesTo(object listener);
        IListener<TEvent> GetListener<TEvent>() where TEvent : class;
    }
}
