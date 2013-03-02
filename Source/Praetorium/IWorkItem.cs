using System;

namespace Praetorium
{
    public interface IWorkItem : ICancelable
    {
        Guid Id { get; }
    }
}
