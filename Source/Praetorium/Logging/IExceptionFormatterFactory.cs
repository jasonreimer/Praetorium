using System;

namespace Praetorium.Logging
{
    public interface IExceptionFormatterFactory
    {

        IExceptionFormatter Get(Type exceptionType);

    }
}
