using System;
using System.Reflection;

namespace Praetorium.Eventing
{
    internal sealed class SubscriptionListener<T> : IListener<T>, IEventSubscription where T : class
    {
        private readonly MethodInfo _method;
        private readonly WeakReference _targetReference;

        public SubscriptionListener(Action<T> handler)
        {
            Ensure.ArgumentNotNull(() => handler);

            _method = handler.Method;

            if (!_method.IsStatic)
                _targetReference = new WeakReference(handler.Target, false);
        }

        public bool IsDead
        {
            get { return _targetReference != null && (!_targetReference.IsAlive || _targetReference.Target == null); }
        }

        public void Handle(T message)
        {
            _method.Invoke(GetTarget(), new object[] { message });
        }

        public bool RelatesTo(object listener)
        {
            return object.ReferenceEquals(listener, this);
        }

        private object GetTarget()
        {   
            return _targetReference == null ? null : _targetReference.Target;
        }

        public Type ListenerType
        {
            get { return GetType(); }
        }

        public IListener<TEvent> GetListener<TEvent>() where TEvent : class
        {
            return this as IListener<TEvent>;
        }
    }
}
