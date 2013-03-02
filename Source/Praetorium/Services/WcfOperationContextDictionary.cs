using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Praetorium.Services
{
    public class WcfOperationContextDictionary<TKey, TValue> : IExtension<OperationContext>
    {

        private Dictionary<TKey, TValue> _values = new Dictionary<TKey, TValue>();

        public Dictionary<TKey, TValue> Values
        {
            get { return _values; }
        }

        void IExtension<OperationContext>.Attach(OperationContext owner)
        {
        }

        void IExtension<OperationContext>.Detach(OperationContext owner)
        {
            _values.Clear();
            _values = null;
        }

        public static WcfOperationContextDictionary<TKey, TValue> Get()
        {
            var operationContext = OperationContext.Current;

            if (operationContext == null)
            {
                throw Errors.NoCurrentWcfOperationContext();
            }

            var operationContextDictionary = operationContext.Extensions.Find<WcfOperationContextDictionary<TKey, TValue>>();

            if (operationContextDictionary == null)
            {
                operationContextDictionary = new WcfOperationContextDictionary<TKey, TValue>();

                operationContext.Extensions.Add(operationContextDictionary);
            }

            return operationContextDictionary;
        }

    }

}
