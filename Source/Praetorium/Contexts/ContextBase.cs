using System.Collections.Generic;

namespace Praetorium.Contexts
{
    public abstract class ContextBase
    {

        public object this[string key]
        {
            get
            {
                return Values[key];
            }
            set
            {
                Values[key] = value;
            }
        }

        public void Add(string key, object value)
        {
            Values.Add(key, value);
        }

        public bool Contains(string key)
        {
            return Values.ContainsKey(key);
        }

        public void Remove(string key)
        {
            Values.Remove(key);
        }

        protected abstract IDictionary<string, object> Values
        {
            get;
        }

    }
}
