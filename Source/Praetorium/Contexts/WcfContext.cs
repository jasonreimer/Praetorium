using Praetorium.Services;
using System.Collections.Generic;

namespace Praetorium.Contexts
{
    public class WcfContext : ContextBase, IActivityContext, ISessionContext
    {
        protected override IDictionary<string, object> Values
        {
            get
            {
                return WcfOperationContextDictionary<string, object>.Get().Values;
            }
        }
    }
}
