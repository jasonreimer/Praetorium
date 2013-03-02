using System;

namespace Praetorium.Logging
{
    public interface ILoggerFactory
    {
        ILogger Get(string source);

        ILogger Get(Type source);

        ILogger Get<TSource>() where TSource : class;
    }
}
