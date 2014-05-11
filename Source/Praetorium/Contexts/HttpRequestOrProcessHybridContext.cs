using Praetorium.Collections;
using System.Collections.Generic;
using System.Web;

namespace Praetorium.Contexts
{
    public class HttpRequestOrProcessHybridContext : ContextBase, IContext
    {
        private static readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        protected override IDictionary<string, object> Values
        {
            get
            {
                var httpContext = HttpContext.Current;

                if (httpContext == null)
                    return _values;

                return new DictionaryWrapper<string, object>(httpContext.Items);
            }
        }
    }
}
