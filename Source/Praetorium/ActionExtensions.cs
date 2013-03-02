using System;

namespace Praetorium
{
    public static class ActionExtensions
    {
        public static void TryAndIgnore(this Action action)
        {
            Ensure.ArgumentNotNull(() => action);

            try
            {
                action();
            }
            catch
            {
            }
        }

        public static void TryAndIgnore<T>(this Action<T> action, T value)
        {
            try
            {
                action(value);
            }
            catch
            {
            }
        }

    }
}
