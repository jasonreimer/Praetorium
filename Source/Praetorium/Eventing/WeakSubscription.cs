using System;

namespace Praetorium.Eventing
{
    internal class WeakSubscription : IEventSubscription
    {
        private readonly WeakReference _listenerReference;
        private readonly Type _listenerType;

        internal WeakSubscription(object listener)
        {
            Ensure.ArgumentNotNull(() => listener);

            _listenerReference = new WeakReference(listener, false);
            _listenerType = listener.GetType();
        }

        public bool IsDead
        {
            get { return !_listenerReference.IsAlive; }
        }

        public Type ListenerType
        {
            get { return _listenerType; }
        }

        public IListener<TEvent> GetListener<TEvent>() where TEvent : class
        {
            return _listenerReference.Target as IListener<TEvent>;
        }

        public bool RelatesTo(object listener)
        {
            return _listenerReference.IsAlive && object.ReferenceEquals(listener, _listenerReference.Target);
        }
    }
}
