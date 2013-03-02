using System;
using System.Web;
using System.Web.SessionState;

namespace Praetorium.Contexts
{
    public class HttpSessionContext : ISessionContext
    {
        public object this[string key]
        {
            get
            {
                return Session[key];
            }
            set
            {
                Session[key] = value;
            }
        }

        public void Add(string key, object value)
        {
            Session.Add(key, value);
        }

        public bool Contains(string key)
        {
            foreach (var item in Session.Keys)
                if (key.Equals(item))
                    return true;

            return false;
        }

        public void Remove(string key)
        {
            Session.Remove(key);
        }

        protected HttpSessionState Session
        {
            get
            {
                var httpContext = HttpContext.Current;

                if (httpContext == null)
                    throw Errors.NoCurrentHttpContext();

                var session = httpContext.Session;

                if (session == null)
                    //TODO: resource
                    throw new InvalidOperationException("There is no current instance of HttpSessionState.");

                return session;
            }
        }
    }
}
