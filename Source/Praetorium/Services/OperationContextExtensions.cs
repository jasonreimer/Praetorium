using System.ServiceModel;
using System.ServiceModel.Description;

namespace Praetorium.Services
{
    public static class OperationContextExtensions
    {
        public static OperationDescription GetOperationDescription(this OperationContext operationContext)
        {
            Ensure.ArgumentNotNull(() => operationContext);

            var hostDescription = operationContext.Host.Description;
            var endpoint = hostDescription.Endpoints.Find(operationContext.IncomingMessageHeaders.To);
            var operationName = operationContext.IncomingMessageHeaders.Action.Replace(endpoint.Contract.Namespace + endpoint.Contract.Name + "/", "");

            return endpoint.Contract.Operations.Find(operationName);
        }
    }
}
