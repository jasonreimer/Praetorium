using System;

namespace Praetorium
{
    public sealed class ActionDisposable : IDisposable
    {
        private readonly Action _action;

        public ActionDisposable(Action action)
        {
            Ensure.ArgumentNotNull(() => action, ref _action);
        }

        public void Dispose()
        {
            _action();
        }
    }
}
