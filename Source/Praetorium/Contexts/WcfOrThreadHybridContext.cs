using Praetorium.Services;
using System;
using System.Collections.Generic;

namespace Praetorium.Contexts
{
    public class WcfOrThreadHybridContext : ThreadContext
    {
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
                    return base.Values;
                }
            }
        }
    }
}
