using System.ServiceModel.Description;

namespace Praetorium.Services
{
    public interface IServiceConfigurer
    {
        void Configure(ServiceDescription description);
    }
}
