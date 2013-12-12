using FluentAssertions;
using Praetorium.Logging;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Extensions;

namespace Praetorium.NLog.Tests
{
    public class NLogLoggerSpecs
    {
        private const string message = "hello";

        private readonly IExceptionFormatterFactory _exceptionFormatterFactory = new ExceptionFormatterFactory(null);
        private readonly TestableNLogLogger _logger;
        
        public NLogLoggerSpecs()
        {
            _logger = new TestableNLogLogger(_exceptionFormatterFactory);
        }

        public static IEnumerable<object[]> LogLevels 
        {
            get 
            {
                var values = Enum.GetValues(typeof(LogLevel));
                var list = new List<object[]>(values.Length);
                
                for (int index = 0; index < values.Length; index++) 
                    list.Add(new object[] { values.GetValue(index) });

                return list;
            }
        }
        
        [Fact]
        public void Logger_should_log_exceptions()
        {
            var exception = new NotImplementedException();

            _logger.Enabled = true;
            _logger.Log(exception);

            _logger.EventLogged.Should().NotBeNull();
        }

        [Fact]
        public void Logger_should_log_exception_with_message()
        {
            var exception = new NotImplementedException();

            _logger.Enabled = true;
            _logger.Log(LogLevel.Error, exception, message);

            _logger.EventLogged.Message.Should().Contain(message);
        }

        [Fact]
        public void Logger_should_not_log_when_disabled()
        {
            var exception = new NotImplementedException();

            _logger.Log(exception);

            _logger.EventLogged.Should().BeNull();
        }

        [Theory]
        [PropertyData("LogLevels")]        
        public void Logger_should_log(LogLevel logLevel)
        {
            _logger.Enabled = true;
            _logger.Log(logLevel, message);

            _logger.EventLogged.Message.Should().Contain(message);
        }

        [Theory]
        [PropertyData("LogLevels")]
        public void Logger_should_not_log_debug(LogLevel logLevel)
        {
            _logger.Log(logLevel, message);
            _logger.EventLogged.Should().BeNull();
        }

        [Fact]
        public void Bogus_log_level_should_throw_argument_exception()
        {
            _logger.Invoking(x => x.Log((LogLevel)100, message))
                   .ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void Logger_should_log_entry()
        {
            var entry = new LogEntry
            {
                Level = LogLevel.Debug,
                Message = message
            };

            _logger.Enabled = true;
            _logger.Log(entry);

            _logger.EventLogged.Message.Should().NotBeEmpty();
        }
    }
}
