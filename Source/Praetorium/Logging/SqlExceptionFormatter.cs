using System;
using System.Data.SqlClient;
using System.IO;

namespace Praetorium.Logging
{
    public class SqlExceptionFormatter : TextExceptionFormatter
    {

        public SqlExceptionFormatter(IExceptionFormatterFactory exceptionFormatterFactory)
            : base(exceptionFormatterFactory)
        {
        }

        protected override void WriteExceptionProperties(Exception exception, TextWriter messageWriter)
        {
            base.WriteExceptionProperties(exception, messageWriter);

            var sqlException = exception as SqlException;

            if (sqlException != null)
                messageWriter
                    .WriteLineIf(sqlException.ErrorCode != 0, VariableFormatExpression, "ErrorCode", sqlException.ErrorCode)
                    .WriteLineIf(sqlException.Class != 0, VariableFormatExpression, "Class", sqlException.Class)
                    .WriteLineIf(sqlException.State != 0, VariableFormatExpression, "State", sqlException.State)
                    .WriteLineIf(sqlException.Number != 0, VariableFormatExpression, "Number", sqlException.Number)
                    .WriteLineIf(sqlException.LineNumber != 0, VariableFormatExpression, "LineNumber", sqlException.LineNumber)
                    .WriteLineIf(sqlException.Server.IsNotNullOrWhiteSpace(), VariableFormatExpression, "Server", sqlException.Server)
                    .WriteLineIf(sqlException.Procedure.IsNotNullOrWhiteSpace(), VariableFormatExpression, "Procedure", sqlException.Procedure);
        }

    }
}
