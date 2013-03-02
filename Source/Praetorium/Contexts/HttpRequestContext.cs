using Praetorium.Collections;
using System.Collections.Generic;
using System.Web;

namespace Praetorium.Contexts
{
    public class HttpContextContext : ContextBase, IActivityContext, ISessionContext
    {
        protected override IDictionary<string, object> Values
        {
            get 
            {
                var httpContext = HttpContext.Current;

                if (httpContext == null)
                    throw Errors.NoCurrentHttpContext();

                return new DictionaryWrapper<string, object>(httpContext.Items);
            }
        }
    }
}
