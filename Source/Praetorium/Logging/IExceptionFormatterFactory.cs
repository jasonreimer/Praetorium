using System;

namespace Praetorium.Logging
{
    public interface IExceptionFormatterFactory
    {

        IExceptionFormatter Get<TException>(TException exception) where TException : Exception;

        IExceptionFormatter Get(Type exceptionType);

    }
}
