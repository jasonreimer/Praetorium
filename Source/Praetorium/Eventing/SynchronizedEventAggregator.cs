using System.Collections.Generic;
using System.Threading;

namespace Praetorium.Eventing
{
    public class SynchronizedEventAggregator : EventAggregator
    {
        private readonly SynchronizationContext _syncContext;

        public SynchronizedEventAggregator(SynchronizationContext syncContext, IListener[] listeners)
            : base(listeners)
        {
            Ensure.ArgumentNotNull(() => syncContext, ref _syncContext);
        }

        protected override void SendToEach<TEvent>(TEvent @event, IEnumerable<IListener<TEvent>> listeners)
        {
            _syncContext.Send(_ => base.SendToEach<TEvent>(@event, listeners), null);
        }
    }
}
