using Praetorium.Contexts;
using System.Collections.Generic;

namespace Praetorium.Logging
{
    public class LogContextScope : ILogContextScope
    {
        private static readonly string _contextKey = typeof(ILogContextScope).FullName;

        private readonly IContext _context;
        private readonly Dictionary<string, object> _items = new Dictionary<string, object>();

        public LogContextScope(IContext context)
        {
            Ensure.ArgumentNotNull(() => context, ref _context);

            _context.Add(_contextKey, this);
        }

        public object this[string key]
        {
            get
            {
                return _items[key];
            }
            set
            {
                _items[key] = value;
            }
        }

        public void Clear()
        {
            _items.Clear();
        }

        public void Dispose()
        {
            Clear();
            _context.Remove(_contextKey);
        }

        public void Remove(string key)
        {
            Ensure.ArgumentNotNullOrWhiteSpace(() => key);

            _items.Remove(key);
        }
    }
}
