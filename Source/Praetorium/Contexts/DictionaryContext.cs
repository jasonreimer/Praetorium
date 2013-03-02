using System.Collections.Generic;

namespace Praetorium.Contexts
{
    public class DictionaryContext : ContextBase, ISessionContext
    {
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        protected override IDictionary<string, object> Values
        {
            get { return _values; }
        }
    }
}
