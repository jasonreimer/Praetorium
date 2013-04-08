using NLog;
using Praetorium.Logging;
using System;

namespace Praetorium.NLog
{
    public class NLogLoggerFactory : ILoggerFactory
    {
        private readonly IExceptionFormatterFactory _exceptionFormatterFactory;

        public NLogLoggerFactory(IExceptionFormatterFactory exceptionFormatterFactory)
        {
            Ensure.ArgumentNotNull(() => exceptionFormatterFactory, ref _exceptionFormatterFactory);
        }

        public ILogger Get<TSource>() where TSource : class
        {
            return Get(typeof(TSource));
        }

        public ILogger Get(Type source)
        {
            return new NLogLogger(LogManager.GetLogger(source.FullName), _exceptionFormatterFactory);
        }

        public ILogger Get(string source)
        {
            return new NLogLogger(LogManager.GetLogger(source), _exceptionFormatterFactory);
        }
    }
}
