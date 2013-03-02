using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Praetorium
{
    public static class DictionaryExtensions
    {

        /// <summary>
        /// Tries to get the value associated with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">When this method returns, contains the value associated with the specified <paramref name="key"/>, if the <paramref name="key"/> is found;
        /// otherwise, null. This parameter is passed uninitialized.</param>
        /// <returns>
        /// <c>true</c> if the <paramref name="dictionary"/> contains an element with the specified <paramref name="key"/>; otherwise, false.
        /// </returns>
        public static bool TryGetValue(this IDictionary dictionary, object key, out object value)
        {
            Ensure.ArgumentNotNull(() => dictionary);

            if (dictionary.Contains(key))
            {
                value = dictionary[key];
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }

        public static bool TryGetValue(this IDictionary dictionary, object key, Action<object> valueAction)
        {
            Ensure.ArgumentNotNull(() => dictionary);
            Ensure.ArgumentNotNull(() => valueAction);

            if (dictionary.Contains(key))
            {
                var value = dictionary[key];

                valueAction(value);
                
                return true;
            }

            return false;
        }

        public static bool TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Action<TValue> valueAction)
        {
            Ensure.ArgumentNotNull(() => dictionary);
            Ensure.ArgumentNotNull(() => valueAction);

            TValue value;

            var found = dictionary.TryGetValue(key, out value);

            if (found)
                valueAction(value);

            return found;        
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return GetValueOrDefault(dictionary, key, default(TValue));
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            Ensure.ArgumentNotNull(() => dictionary);

            var value = default(TValue);

            if (!dictionary.TryGetValue(key, out value))
                value = defaultValue;

            return value;
        }

        public static object GetValueOrDefault(this IDictionary dictionary, object key)
        {
            Ensure.ArgumentNotNull(() => dictionary);

            return dictionary.Contains(key) ? dictionary[key] : null;
        }

        public static T GetValueOrDefault<T>(this IDictionary dictionary, object key, T defaultValue)
        {
            Ensure.ArgumentNotNull(() => dictionary);

            return dictionary.Contains(key) ? (T)dictionary[key] : defaultValue;
        }

        /// <summary>
        /// Tries to get the value associated with the specified <paramref name="key"/>, and adds the specified
        /// <paramref name="defaultValue"/> if the <paramref name="key"/> is not found.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// If the specified <paramref name="key"/> is found, the value associated is returned; otherwise,
        /// the <paramref name="defaultValue"/> is added to the <paramref name="dictionary"/> and returned.
        /// </returns>
        public static object GetOrAdd(this IDictionary dictionary, object key, object defaultValue)
        {
            Ensure.ArgumentNotNull(() => dictionary);

            object value = null;

            if (dictionary.Contains(key))
            {
                value = dictionary[key];
            }
            else
            {
                value = defaultValue;
                dictionary.Add(key, value);
            }

            return value;
        }

        /// <summary>
        /// Tries to get the value associated with the specified <paramref name="key"/>, and adds the value
        /// return from the specified <paramref name="defaultValueHandler"/> if the <paramref name="key"/> is not found.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="defaultValueHandler">An expression that returns the default value.</param>
        /// <returns>
        /// If the specified <paramref name="key"/> is found, the value associated is returned; otherwise,
        /// the value returned from the <paramref name="defaultValueHandler"/> is added to the <paramref name="dictionary"/> and returned.
        /// </returns>
        public static object GetOrAdd(this IDictionary dictionary, object key, Func<object> defaultValueHandler)
        {
            Ensure.ArgumentNotNull(() => dictionary);

            object value = null;

            if (dictionary.Contains(key))
            {
                value = dictionary[key];
            }
            else
            {
                value = defaultValueHandler();
                dictionary.Add(key, value);
            }

            return value;
        }

        /// <summary>
        /// Tries to get the value associated with the specified <paramref name="key"/>, and adds the value
        /// return from the specified <paramref name="defaultValueHandler"/> if the <paramref name="key"/> is not found.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="defaultValueHandler">An expression that returns the default value.</param>
        /// <returns>
        /// If the specified <paramref name="key"/> is found, the value associated is returned; otherwise,
        /// the value returned from the <paramref name="defaultValueHandler"/> is added to the <paramref name="dictionary"/> and returned.
        /// </returns>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> defaultValueHandler)
        {
            Ensure.ArgumentNotNull(() => dictionary);

            var value = default(TValue);

            if (!dictionary.TryGetValue(key, out value))
            {
                value = defaultValueHandler();
                dictionary.Add(key, value);
            }

            return value;
        }

        public static TValue GetOrAddNew<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
            where TValue : new()
        {
            return dictionary.GetOrAdd(key, () => new TValue());
        }

        /// <summary>
        /// Tries to get the value associated with the specified <paramref name="key"/>, and adds the specified
        /// <paramref name="defaultValue"/> if the <paramref name="key"/> is not found.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// If the specified <paramref name="key"/> is found, the value associated is returned; otherwise,
        /// the <paramref name="defaultValue"/> is added to the <paramref name="dictionary"/> and returned.
        /// </returns>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            Ensure.ArgumentNotNull(() => dictionary);

            var value = default(TValue);

            if (!dictionary.TryGetValue(key, out value))
            {
                value = defaultValue;
                dictionary.Add(key, value);
            }

            return value;
        }

        public static void CopyTo<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> destination, params TKey[] excludeKeys)
        {
            Ensure.ArgumentNotNull(() => source);
            Ensure.ArgumentNotNull(() => destination);

            foreach (var pair in source)
                if (!excludeKeys.Contains(pair.Key))
                    destination[pair.Key] = pair.Value;
        }

        public static void CopyTo(this IDictionary<string, object> source, IDictionary destination, params string[] excludeKeys)
        {
            Ensure.ArgumentNotNull(() => source);
            Ensure.ArgumentNotNull(() => destination);

            foreach (var pair in source)
                if (!excludeKeys.Contains(pair.Key))
                    destination[pair.Key] = pair.Value;
        }

        public static void AddChild<TKey, TValue, TChildValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TChildValue childValue)
            where TValue : ICollection<TChildValue>, new()
        {
            dictionary.GetOrAddNew(key).Add(childValue);
        }

        public static void AddChild<TKey, TValue, TChildValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TKey childKey, TChildValue childValue)
            where TValue : IDictionary<TKey, TChildValue>, new()
        {
            dictionary.GetOrAddNew(key).Add(childKey, childValue);
        }

    }
}
