using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Praetorium.Collections
{
    public class DictionaryWrapper<TKey, TValue> : IDictionary<TKey, TValue>
    {

        private readonly IDictionary _innerDictionary;

        public DictionaryWrapper(IDictionary innerDictionary)
        {
            Ensure.ArgumentNotNull(() => innerDictionary, ref _innerDictionary);
        }

        public void Add(TKey key, TValue value)
        {
            _innerDictionary.Add(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            return _innerDictionary.Contains(key);
        }

        public ICollection<TKey> Keys
        {
            get { return _innerDictionary.Keys.Cast<TKey>().ToList(); }
        }

        public bool Remove(TKey key)
        {
            var contained = _innerDictionary.Contains(key);

            if (contained)
            {
                _innerDictionary.Remove(key);
            }

            return contained;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            object innerValue = null;

            bool found = _innerDictionary.TryGetValue(key, out innerValue);

            value = (TValue)innerValue;

            return found;
        }

        public ICollection<TValue> Values
        {
            get { return _innerDictionary.Values.Cast<TValue>().ToList(); }
        }

        public TValue this[TKey key]
        {
            get
            {
                return (TValue)_innerDictionary[key];
            }
            set
            {
                _innerDictionary[key] = value;
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            _innerDictionary.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _innerDictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _innerDictionary.Contains(item.Key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _innerDictionary.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _innerDictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return _innerDictionary.IsReadOnly; }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            bool contained = Contains(item);

            if (contained)
            {
                _innerDictionary.Remove(item.Key);
            }

            return contained;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerDictionary.GetEnumerator();
        }

        protected IDictionary InnerDictionary
        {
            get { return _innerDictionary; }
        }
    }
}
