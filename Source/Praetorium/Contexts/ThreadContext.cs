using System;
using System.Collections.Generic;

namespace Praetorium.Contexts
{
    public class ThreadContext : ContextBase, IActivityContext, ISessionContext
    {
        [ThreadStatic]
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        protected override IDictionary<string, object> Values
        {
            get { return _values; }
        }
    }
}
