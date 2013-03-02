using System;

namespace Praetorium.Contexts
{
    public static class ContextExtensions
    {
        public static void TryAdd(this IContext context, string key, object item, Action failureAction = null)
        {
            Ensure.ArgumentNotNull(() => context);
            Ensure.ArgumentNotNullOrWhiteSpace(() => key);

            if (!context.Contains(key))
                context[key] = item;
            else
                if (failureAction != null)
                    failureAction();
        }

        public static T GetOrDefault<T>(this IContext context, string key)
        {
            Ensure.ArgumentNotNull(() => context);
            Ensure.ArgumentNotNullOrWhiteSpace(() => key);

            T value = default(T);

            if (context.Contains(key))
            {
                try
                {
                    value = (T)context[key];
                }
                catch
                {
                }
            }

            return value;
        }
    }
}
