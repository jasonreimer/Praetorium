using System;
using System.Collections;
using System.IO;

namespace Praetorium.Logging
{

    /// <summary>
    /// Contains utility methods that format exception output for logging purposes.
    /// </summary>
    public class TextExceptionFormatter : IExceptionFormatter
    {

        private readonly IExceptionFormatterFactory _factory;

        public TextExceptionFormatter(IExceptionFormatterFactory exceptionFormatterFactory)
        {
            Ensure.ArgumentNotNull(() => exceptionFormatterFactory, ref _factory);
        }

        /// <summary>
        /// Gets the format expression used when writing variables to the writer.
        /// </summary>
        /// <remarks>
        /// If overriden by a derived class, the format expression must contain 
        /// only two variables placeholders, where {0} is the variable key, and
        /// {1} is the variable value.
        /// </remarks>
        protected virtual string VariableFormatExpression
        {
            get { return "{0}={1}"; }
        }

        protected IExceptionFormatterFactory FormatterFactory
        {
            get { return _factory; }
        }

        /// <summary>
        /// Writes the formatted exception data to the <paramref name="writer"/>.
        /// </summary>
        /// <param name="exception">
        /// The exception instance.
        /// </param>
        /// <param name="writer">
        /// The output writer.
        /// </param>
        public void Write(Exception exception, TextWriter writer)
        {
            WriteExceptionInfo(exception, writer);
        }

        /// <summary>
        /// Determines if the <paramref name="exception" /> has an Errors property,
        /// and if so, writes the error information to the <paramref name="messageWriter" />.
        /// </summary>
        /// <param name="exception">
        /// The exception instance being handled.
        /// </param>
        /// <param name="messageWriter">
        /// The output writer.
        /// </param>
        public virtual void WriteErrorInfo(Exception exception, TextWriter messageWriter)
        {
            Ensure.ArgumentNotNull(() => exception);
            Ensure.ArgumentNotNull(() => messageWriter);

            var errorsCollection = GetErrorsCollection(exception);

            if (errorsCollection != null)
            {
                messageWriter.WriteLine();
                messageWriter.WriteLine("Error Messages:");
                messageWriter.WriteLine();

                foreach (object error in errorsCollection)
                {
                    messageWriter.WriteLine(error.ToString());
                }

                messageWriter.WriteLine();
            }
        }

        /// <summary>
        /// Formats and writes the exception information to the <paramref name="messageWriter" />.
        /// </summary>
        /// <param name="exception">
        /// The exception instance being handled.
        /// </param>
        /// <param name="messageWriter">
        /// The output writer.
        /// </param>
        public virtual void WriteExceptionInfo(Exception exception, TextWriter messageWriter)
        {
            Ensure.ArgumentNotNull(() => exception);
            Ensure.ArgumentNotNull(() => messageWriter);

            WriteExceptionProperties(exception, messageWriter);
            WriteErrorInfo(exception, messageWriter);
            WriteVariables(exception.Data, messageWriter, "Exception Data:");

            // recurse the exception tree
            WriteInnerException(exception, messageWriter);
        }

        /// <summary>
        /// Writes the <paramref name="exception"/>'s properties to the <paramref name="messageWriter" />.
        /// </summary>
        /// <param name="exception">
        /// The exception instance being handled.
        /// </param>
        /// <param name="messageWriter">
        /// The output writer.
        /// </param>
        protected virtual void WriteExceptionProperties(Exception exception, TextWriter messageWriter)
        {
            Ensure.ArgumentNotNull(() => exception);
            Ensure.ArgumentNotNull(() => messageWriter);

            messageWriter.WriteLine("Exception: {0}", exception.GetType().FullName);
            messageWriter.WriteLine("Message: {0}", exception.Message);

#if !SILVERLIGHT
            if (exception.Source.IsNotNullOrWhiteSpace())
            {
                messageWriter.WriteLine("Source: {0}", exception.Source);
            }
#endif

            if (exception.StackTrace.IsNotNullOrWhiteSpace())
            {
                messageWriter.WriteLine(exception.StackTrace);
                messageWriter.WriteLine();
            }
        }

        /// <summary>
        /// Writes <paramref name="exception" />'s InnerException if one exists.
        /// </summary>
        /// <param name="exception">
        /// The exception instance being handled.
        /// </param> 
        /// <param name="messageWriter">
        /// The output writer.
        /// </param>
        /// <remarks>
        /// This method causes an indirect recursion of the exception chain.
        /// </remarks>
        protected virtual void WriteInnerException(Exception exception, TextWriter messageWriter)
        {
            Ensure.ArgumentNotNull(() => exception);
            Ensure.ArgumentNotNull(() => messageWriter);

            if (exception.InnerException != null)
            {
                messageWriter.WriteLine();
                messageWriter.WriteLine("Inner Exception:");
                messageWriter.WriteLine();

                var formatter = _factory.Get(exception.InnerException);
                formatter.Write(exception.InnerException, messageWriter);
            }
        }

        /// <summary>
        /// Formats and writes the key/value pairs to the <paramref name="messageWriter" />.
        /// </summary>
        /// <param name="variables">
        /// A dictionary that contains the key/value pairs for the additional data.
        /// </param>
        /// <param name="messageWriter">
        /// The output writer.
        /// </param>
        /// <param name="headerLine">
        /// The line that preceeds the variables output.
        /// </param>
        protected virtual void WriteVariables(IDictionary variables, TextWriter messageWriter, string headerLine)
        {
            if (variables != null && variables.Count > 0)
            {
                var variableFormat = VariableFormatExpression;

                messageWriter.WriteLine();
                messageWriter.WriteLine(headerLine);
                messageWriter.WriteLine();

                foreach (DictionaryEntry entry in variables)
                {
                    messageWriter.WriteLine(variableFormat, entry.Key, entry.Value);
                }

                messageWriter.WriteLine();
            }
        }

        /// <summary>
        /// Uses reflection on the <paramref name="exception" /> to get the 
        /// Errors property collection.
        /// </summary>
        /// <param name="exception">
        /// The exception instance being handled.
        /// </param>
        /// <returns>
        /// If the <paramref name="exception" /> has an Errors property of type ICollection, 
        /// the property value is returned; if not, null is returned.
        /// </returns>
        protected IEnumerable GetErrorsCollection(Exception exception)
        {
            IEnumerable errors = null;

            try
            {
                var property = exception.GetType().GetProperty("Errors");

                if (property != null)
                {
                    errors = property.GetValue(exception, null) as IEnumerable;
                }
            }
            catch
            {
            }

            return errors;
        }

    }
}
