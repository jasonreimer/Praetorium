using System;
using System.ServiceModel.Channels;

namespace Praetorium.Services
{
    public class FaultFactory : FactoryBase<IFaultBuilder, Exception, MessageFault>, IFaultFactory
    {
        public FaultFactory(IFaultBuilder[] builders)
            : base(builders)
        {
        }
    }
}
