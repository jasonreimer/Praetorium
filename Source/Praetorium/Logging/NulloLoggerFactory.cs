using System;

namespace Praetorium.Logging
{
    public class NulloLoggerFactory : ILoggerFactory
    {
        public ILogger Get(string source)
        {
            return new NulloLogger();
        }

        public ILogger Get(Type source)
        {
            return new NulloLogger();
        }

        public ILogger Get<TSource>() where TSource : class
        {
            return new NulloLogger();
        }
    }
}
