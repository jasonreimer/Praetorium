using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace Praetorium.Services
{
    public class InjectedServiceHostFactory : ServiceHostFactory
    {
        public static Func<Type, Uri[], ServiceHost> Factory = 
            (t, u) => { throw new InvalidOperationException("The InjectedServiceHostFactory.Factory field is not initialized."); };

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return Factory(serviceType, baseAddresses);
        }
    }
}
