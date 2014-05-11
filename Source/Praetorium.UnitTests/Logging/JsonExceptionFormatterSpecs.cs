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

namespace Praetorium.UnitTests.Logging
{
    public class JsonExceptionFormatterSpecs
    {
        [Fact]
        public void Formatter_should_be_found_by_factory()
        {
            var builder = new ExceptionFormatterBuilder<Exception, JsonExceptionFormatter>(f => new JsonExceptionFormatter());
            var factory = new ExceptionFormatterFactory(new[] { builder });
            string message = null;

            try
            {
                throw CreateException();
            }
            catch (Exception ex)
            {
                message = factory.Format(ex);
            }

            Trace.WriteLine(message);

            message.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void Formatter_write_should_output_exception_info_to_writer()
        {
            var writer = new StringWriter();
            var formatter = new JsonExceptionFormatter();

            try
            {
                throw CreateException();
            }
            catch (Exception ex)
            {
                formatter.Write(ex, writer);
            }

            var output = writer.ToString();

            Trace.WriteLine(output);

            output.Should().NotBeNullOrWhiteSpace();
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
