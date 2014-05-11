using System;
using System.IO;

namespace Praetorium.Logging
{
    public interface IExceptionFormatter
    {
        void Write(Exception exception, TextWriter writer);
    }
}
