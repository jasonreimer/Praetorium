namespace Praetorium.Services
{
    internal static class ServiceTypeCache<TService> where TService : class
    {
        public static readonly ServiceType<TService> Instance = new ServiceType<TService>();
    }
}
