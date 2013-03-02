using System;
using System.ServiceModel;

namespace Praetorium.Services
{
    public class ConfigurerServiceHost : ServiceHost
    {
        private readonly IServiceConfigurer _serviceConfigurer;

        public ConfigurerServiceHost(IServiceConfigurer serviceConfigurer, Type serviceType, Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            Ensure.ArgumentNotNull(() => serviceConfigurer, ref _serviceConfigurer);
        }

        protected override void OnOpening()
        {
            _serviceConfigurer.Configure(Description);

            base.OnOpening();
        }
    }
}
