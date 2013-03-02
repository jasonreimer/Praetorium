using System.Collections.Generic;

namespace Praetorium.Contexts
{
    public interface IContextBridge
    {
        void GetValuesFromContext(IDictionary<string, object> values);
        void SetValuesToContext(IDictionary<string, object> values);
    }
}
