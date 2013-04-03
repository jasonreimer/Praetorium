using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Praetorium
{
    public static class KeyedCollectionExtensions
    {

        public static bool TryGetValue<TKey, TItem>(this KeyedCollection<TKey, TItem> collection, TKey key, out TItem item)
        {
            Ensure.ArgumentNotNull(() => collection);

            try
            {
                item = collection[key];

                return true;
            }
            catch
            {
                item = default(TItem);

                return false;
            }
        }

        public static TItem GetValueOrDefault<TKey, TItem>(this KeyedCollection<TKey, TItem> collection, TKey key)
        {
            Ensure.ArgumentNotNull(() => collection);

            try
            {
                return collection[key];
            }
            catch
            {
                return default(TItem);
            }
        }

        public static TItem GetValueOrDefault<TKey, TItem>(this Dictionary<TKey, TItem> collection, TKey key)
        {
            Ensure.ArgumentNotNull(() => collection);

            try
            {
                return collection[key];
            }
            catch
            {
                return default(TItem);
            }
        }

        public static TItem GetOrAddNew<TKey, TItem>(this KeyedCollection<TKey, TItem> collection, TKey key)
            where TItem : new()
        {
            return collection.GetOrAdd(key, () => new TItem());
        }

        public static TItem GetOrAdd<TKey, TItem>(this KeyedCollection<TKey, TItem> collection, TKey key, Func<TItem> defaultValueHandler)
        {
            Ensure.ArgumentNotNull(() => collection);

            var value = default(TItem);

            if (collection.Contains(key))
            {
                value = collection[key];
            }
            else
            {
                value = defaultValueHandler();
                collection.Add(value);
            }

            return value;
        }

        public static void AddChild<TKey, TValue, TChildValue>(this KeyedCollection<TKey, TValue> collection, TKey key, TChildValue childValue)
            where TValue : ICollection<TChildValue>, new()
        {
            collection.GetOrAddNew(key).Add(childValue);
        }

        public static TChildValue GetChildOrDefault<TKey, TValue, TChildKey, TChildValue>(this KeyedCollection<TKey, TValue> collection, TKey parentKey, TChildKey childKey)
            where TValue : KeyedCollection<TChildKey, TChildValue>
        {
            if (collection.Contains(parentKey))
            {
                var childCollection = collection[parentKey];

                return childCollection.Contains(childKey) ? childCollection[childKey] : default(TChildValue);
            }

            return default(TChildValue);
        }

        public static TChildValue GetFirstChildOrDefault<TKey, TValue, TChildValue>(this KeyedCollection<TKey, TValue> collection, TKey key)
            where TValue : IEnumerable<TChildValue>
        {
            return collection.Contains(key) ? collection[key].FirstOrDefault() : default(TChildValue);
        }

        public static TChildValue GetFirstChildOrDefault<TKey, TValue, TChildValue>(this KeyedCollection<TKey, TValue> collection, TKey key, Func<TChildValue, bool> predicate)
            where TValue : IEnumerable<TChildValue>
        {
            return collection.Contains(key) ? collection[key].FirstOrDefault(predicate) : default(TChildValue);
        }
    }
}
