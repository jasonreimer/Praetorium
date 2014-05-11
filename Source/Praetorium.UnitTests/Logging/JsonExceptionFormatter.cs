using Newtonsoft.Json;
using Praetorium.Logging;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters;

namespace Praetorium.UnitTests.Logging
{
    public class JsonExceptionFormatter : IExceptionFormatter
    {
        public void Write(Exception exception, TextWriter writer)
        {
            writer.WriteLine("Exception: {0}", exception.GetType().FullName);
            writer.WriteLine("Message: {0}", exception.Message);

            if (exception.StackTrace.IsNotNullOrWhiteSpace())
            {
                writer.WriteLine(exception.StackTrace);
                writer.WriteLine();
            }

            try
            {
                var serializer = new JsonSerializer
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                    TypeNameHandling = TypeNameHandling.None,
                    TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
                    PreserveReferencesHandling = PreserveReferencesHandling.None,
                };

                serializer.Serialize(writer, exception);
            }
            catch (Exception ex)
            {
                writer.Write("unable to format exception details because of the following failure: {0}", ex);
            }
        }
    }
}
