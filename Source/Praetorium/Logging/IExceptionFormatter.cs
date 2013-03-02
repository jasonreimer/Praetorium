using System;
using System.IO;

namespace Praetorium.Logging
{
    public interface IExceptionFormatter
    {

        void Write(Exception exception, TextWriter writer);

        void WriteErrorInfo(Exception exception, TextWriter messageWriter);

        void WriteExceptionInfo(Exception exception, TextWriter writer);

    }
}
