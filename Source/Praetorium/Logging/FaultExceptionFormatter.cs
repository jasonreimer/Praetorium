using System;
using System.IO;
using System.ServiceModel;

namespace Praetorium.Logging
{
    public class FaultExceptionFormatter : TextExceptionFormatter
    {

        public FaultExceptionFormatter(IExceptionFormatterFactory exceptionFormatterFactory)
            : base(exceptionFormatterFactory)
        {
        }

        protected override void WriteExceptionProperties(Exception exception, TextWriter messageWriter)
        {
            base.WriteExceptionProperties(exception, messageWriter);

            var faultException = exception as FaultException;

            if (faultException != null) 
                messageWriter
                    .WriteLineIf(faultException.Reason != null, "Reason: {0}", faultException.Reason)
                    .WriteLineIf(faultException.Code != null && faultException.Code.Name.IsNotNullOrWhiteSpace(), 
                                 "Code: {0}", faultException.Code.Name)
                    .WriteLineIf(faultException.Action.IsNotNullOrWhiteSpace(), "Action: {0}", faultException.Action);
        }

    }
}
