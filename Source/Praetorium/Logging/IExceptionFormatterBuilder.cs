using System;

namespace Praetorium.Logging
{
    public interface IExceptionFormatterBuilder
    {

        Type BaseExceptionType { get; }

        IExceptionFormatter Get();

    }
}
