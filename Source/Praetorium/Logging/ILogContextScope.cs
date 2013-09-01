using System;

namespace Praetorium.Logging
{
    public interface ILogContextScope : IDisposable
    {
        object this[string key] { get; set; }

        void Clear();

        void Remove(string key);
    }
}
