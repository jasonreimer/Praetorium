﻿using Praetorium.Collections;
using System.Collections.Generic;
using System.Web;

namespace Praetorium.Contexts
{
    public class HttpRequestOrThreadHybridContext : ThreadContext
    {
        protected override IDictionary<string, object> Values
        {
            get
            {
                var httpContext = HttpContext.Current;

                if (httpContext == null)
                    return base.Values;

                return new DictionaryWrapper<string, object>(httpContext.Items);
            }
        }
    }
}
