using System;
using System.Diagnostics;

namespace Praetorium.Logging
{
    public class TraceLoggerFactory : ILoggerFactory
    {
        private readonly IExceptionFormatterFactory _exceptionFormatterFactory;

        public TraceLoggerFactory(IExceptionFormatterFactory exceptionFormatterFactory)
        {
            Ensure.ArgumentNotNull(() => exceptionFormatterFactory, ref _exceptionFormatterFactory);
        }

        internal TraceLoggerFactory()
            : this(new ExceptionFormatterFactory())
        {
        }

        public ILogger Get(string source)
        {
            Ensure.ArgumentNotNullOrWhiteSpace(() => source);

            return new TraceLogger(new TraceSource(source), _exceptionFormatterFactory);
        }

        public ILogger Get(Type source)
        {
            Ensure.ArgumentNotNull(() => source);

            return new TraceLogger(new TraceSource(source.FullName), _exceptionFormatterFactory);
        }

        public ILogger Get<TSource>() where TSource : class
        {
            return Get(typeof(TSource));
        }
    }
}
