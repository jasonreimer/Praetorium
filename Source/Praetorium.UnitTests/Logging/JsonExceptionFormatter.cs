using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Praetorium.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Linq;
using System.Reflection;
using System.Collections.Concurrent;

namespace Praetorium.UnitTests.Logging
{
    public class JsonExceptionFormatter : IExceptionFormatter
    {
        private readonly IExceptionFormatterFactory _factory;

        public JsonExceptionFormatter(IExceptionFormatterFactory exceptionFormatterFactory)
        {
            Ensure.ArgumentNotNull(() => exceptionFormatterFactory, ref _factory);

            VariableFormat = "{0}: {1}";
        }

        public string VariableFormat { get; set; }

        public void Write(Exception exception, TextWriter writer)
        {
            writer.WriteLine(VariableFormat, "Exception", exception.GetType().FullName);
            writer.WriteLine(VariableFormat, "Message", exception.Message);
            writer.WriteLine(VariableFormat, "Source", exception.Source);

            if (exception.StackTrace.IsNotNullOrWhiteSpace())
            {
                writer.WriteLine(exception.StackTrace);
                writer.WriteLine();
            }

            WriteExceptionProperties(exception, writer);
            WriteInnerException(exception, writer);
        }

        protected virtual void WriteExceptionProperties(Exception exception, TextWriter writer)
        {
            try
            {
                writer.WriteLine("Additional details:");

                var serializer = new JsonSerializer
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                    TypeNameHandling = TypeNameHandling.None,
                    TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
                    PreserveReferencesHandling = PreserveReferencesHandling.None,
                    ContractResolver = new ExceptionContractResolver(),

                };
                serializer.Error += (s, e) => { };
                serializer.Serialize(writer, exception);
                writer.WriteLine();
            }
            catch (Exception ex)
            {
                writer.Write("unable to format exception details because of the following failure: {0}", ex);
            }
        }

        protected virtual void WriteInnerException(Exception exception, TextWriter writer)
        {
            Ensure.ArgumentNotNull(() => exception);
            Ensure.ArgumentNotNull(() => writer);

            if (exception.InnerException == null)
                return;

            writer.WriteLine();
            writer.WriteLine("Inner Exception:");
            writer.WriteLine();

            var formatter = _factory.Get(exception.InnerException.GetType());
            formatter.Write(exception.InnerException, writer);
        }
    }

    public class ExceptionContractResolver : DefaultContractResolver
    {
        private static readonly string[] _ignoredPropertyNames =
            new[] { "Message", "InnerException", "TargetSite", "Source", "StackTrace" };

        //private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _propertyCache =
        //    new ConcurrentDictionary<Type, PropertyInfo[]>();

        private static readonly object _locker = new object();

        public ExceptionContractResolver()
        {
            IgnoreSerializableAttribute = true;
            IgnoreSerializableInterface = true;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (_ignoredPropertyNames.Contains(property.PropertyName))
                property.Ignored = true;

            return property;
        }
    }
}
