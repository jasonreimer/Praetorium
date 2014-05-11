using System;
using System.Collections.Generic;

namespace Praetorium.Contexts
{
    public class ThreadContext : ContextBase, IActivityContext, ISessionContext
    {
        [ThreadStatic]
        private static Dictionary<string, object> _values;

        private static readonly object _locker = new object();

        protected override IDictionary<string, object> Values
        {
            get 
            {
                lock (_locker)
                {
                    if (_values == null)
                        _values = new Dictionary<string, object>();
                }

                return _values; 
            }
        }
    }
}
