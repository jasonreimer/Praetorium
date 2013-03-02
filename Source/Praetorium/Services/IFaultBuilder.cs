using System;
using System.ServiceModel.Channels;

namespace Praetorium.Services
{
    public interface IFaultBuilder : IBuilder<Exception, MessageFault>
    {
    }
}
