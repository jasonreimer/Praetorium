using Praetorium.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Praetorium.Services
{
    public class ErrorHandler : IErrorHandler
    {

        private readonly ILogger _logger;
        private readonly IFaultFactory _faultFactory;

        public ErrorHandler(ILoggerFactory loggerFactory, IFaultFactory faultFactory)
        {
            Ensure.ArgumentNotNull(() => faultFactory, ref _faultFactory);
            Ensure.ArgumentNotNull(() => loggerFactory);

            _logger = loggerFactory.Get<ErrorHandler>();
        }

        public bool HandleError(Exception error)
        {
            _logger.Log(error);

            return true;
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (error != null && error is FaultException)
                return;

            try
            {
                var messageFault = _faultFactory.Create(error);
                var description = OperationContext.Current.GetOperationDescription();
                var action = description != null ? description.Name : "default";

                // return the message fault
                fault = Message.CreateMessage(version, messageFault, action);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Debug, ex, "Handled unexpected exception in the error handler.");
            }
        }
    }

}
