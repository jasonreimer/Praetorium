using Praetorium.Services;
using System;
using System.Collections.Generic;

namespace Praetorium.Contexts
{
    public class WcfOrThreadHybridContext : ContextBase, IActivityContext, ISessionContext
    {
        [ThreadStatic]
        private static readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        protected override IDictionary<string, object> Values
        {
            get
            {
                try
                {
                    return WcfOperationContextDictionary<string, object>.Get().Values;
                }
                catch (InvalidOperationException)
                {
                    return _values;
                }
            }
        }
    }
}
