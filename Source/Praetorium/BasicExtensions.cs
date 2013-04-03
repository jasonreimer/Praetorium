using System;

namespace Praetorium
{
    public static class BasicExtensions
    {
        public static T If<T>(this T t, bool condition, Action<T> action)
        {
            if (condition)
                action(t);

            return t;
        }


        public static R IfNotNull<T, R>(this T t, Func<T, R> action) where T : class
        {
            if (t != null)
                return action(t);

            return default(R);
        }

        public static TOut IfNull<TTarget, TOut>(this TTarget target, Func<TTarget, TOut> valueFunc)
        {
            Ensure.ArgumentNotNull(() => valueFunc);

            return target == null ? valueFunc(target) : default(TOut);
        }

        public static T IfNotNull<T>(this object target, Func<T> valueFunc) where T : class
        {
            Ensure.ArgumentNotNull(() => valueFunc);

            return target == null ? null : valueFunc();
        }

        public static T? IfNotNull<T>(this T? target, Action<T> action) where T : struct
        {
            if (target.HasValue)
            {
                Ensure.ArgumentNotNull(() => action);
                action(target.Value);
            }

            return target;
        }

        public static T IfNotNull<T>(this T target, Action<T> action) where T : class
        {
            if (target != null)
            {
                Ensure.ArgumentNotNull(() => action);
                action(target);
            }

            return target;
        }
    }
}
