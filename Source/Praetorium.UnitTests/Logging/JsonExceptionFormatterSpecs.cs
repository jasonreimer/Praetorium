using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Praetorium.Logging;
using Newtonsoft.Json.Serialization;

namespace Praetorium.UnitTests.Logging
{
    public class JsonExceptionFormatterSpecs
    {
        [Fact]
        public void Formatter_should_be_found_by_factory()
        {
            var builder = new ExceptionFormatterBuilder<Exception, JsonExceptionFormatter>(f => new JsonExceptionFormatter(f));
            var factory = new ExceptionFormatterFactory(new[] { builder });
            string message = null;

            try
            {
                //try
                //{
                //    var ex = new InvalidCastException("this didn't work");

                //    ex.Data.Add("error-code", 12345);

                //    throw ex;
                //}
                //catch (Exception inner)
                //{
                //    throw new InvalidOperationException("can't do that", inner);
                //}

                throw CreateException();
            }
            catch (Exception ex)
            {
                message = factory.Format(ex);
            }

            Trace.WriteLine(message);

            message.Should().NotBeNullOrWhiteSpace();
        }

        private Exception CreateException()
        {
            var fault = new Fault
            {
                Message = "hello",
                ErrorCode = "B29",
                Inner = new Fault
                {
                    Message = "there",
                    ErrorCode = "oops"
                }
            };

            var ex = new FaultException<Fault>(fault, "this is the reason");

            ex.Data.Add("key1", "value");
            ex.Data.Add("key2", "value");
            ex.Data.Add("key3", "value");

            return ex;
        }

        public class Fault
        {
            public string Message { get; set; }
            public string ErrorCode { get; set; }
            public Fault Inner { get; set; }
        }
    }
}
