using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Praetorium
{
    public static class CollectionExtensions
    {

        public static int Count(this IEnumerable enumerable)
        {
            if (enumerable is ICollection)
                return ((ICollection)enumerable).Count;

            var count = 0;
            var enumerator = enumerable.GetEnumerator();

            while (enumerator.MoveNext())
                count++;

            return count;
        }

        public static int Count(this IEnumerable enumerable, Func<object, bool> predicate)
        {
            Ensure.ArgumentNotNull(() => enumerable);
            Ensure.ArgumentNotNull(() => predicate);

            if (enumerable is ICollection)
                return ((ICollection)enumerable).Count;

            var count = 0;

            foreach (var item in enumerable)
                if (predicate(item))
                    count++;

            return count;
        }

        public static bool IsNotEmpty<T>(this ICollection<T> collection)
        {
            return collection.Count != 0;
        }

        public static bool IsEmpty<T>(this ICollection<T> collection)
        {
            return collection.Count == 0;
        }

        public static T PushNew<T>(this Stack<T> stack) where T : new()
        {
            var t = new T();

            stack.Push(t);

            return t;
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            return ForEach(collection, action, true);
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action, bool ignoreNullCollection)
        {
            Ensure.ArgumentNotNull(() => action);

            if (!ignoreNullCollection)
                Ensure.ArgumentNotNull(() => collection);

            foreach (var item in collection)
                action(item);

            return collection;
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> collection, params T[] items)
        {
            return collection.Concat((IEnumerable<T>)items);
        }

        public static IEnumerable<T> ConcatNonNulls<T>(this IEnumerable<T> collection, params T[] items)
        {
            items = items ?? new T[0];

            return collection.Concat(items.Where(x => x != null));
        }

        public static ICollection<T> AddMany<T>(this ICollection<T> collection, params T[] items)
        {
            items.ForEach(collection.Add);

            return collection;
        }

        public static int FirstIndexOf<TItem>(this IEnumerable<TItem> items, Func<TItem, bool> predicate)
        {
            Ensure.ArgumentNotNull(() => items);

            int index = 0;

            foreach (TItem item in items)
            {
                if (predicate(item))
                    return index;

                index++;
            }

            return -1;
        }

        public static int FirstIndexOf(this IEnumerable items, Func<object, bool> predicate)
        {
            Ensure.ArgumentNotNull(() => items);

            int index = 0;

            foreach (object item in items)
            {
                if (predicate(item))
                    return index;

                index++;
            }

            return -1;
        }

        public static bool None<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            Ensure.ArgumentNotNull(() => items);
            Ensure.ArgumentNotNull(() => predicate);

            foreach (T item in items)
                if (predicate(item))
                    return false;

            return true;
        }
    }
}
