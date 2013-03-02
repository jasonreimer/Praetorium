using System;
using System.ServiceModel.Channels;

namespace Praetorium.Services
{
    public interface IFaultFactory
    {
        MessageFault Create(Exception exception);
    }
}
