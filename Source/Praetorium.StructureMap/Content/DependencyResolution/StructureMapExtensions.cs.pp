using Praetorium;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace $rootnamespace$.DependencyResolution
{
    public static class StructureMapExtensions
    {
        public static SmartInstance<T> RegisterSelf<T>(this IInitializationExpression expression) where T : class
        {
            return expression.For<T>().Use<T>();
        }

        public static SmartInstance<T> RegisterSelf<T>(this IRegistry registry)
        {
            return registry.For<T>().Use<T>();
        }

        public static SmartInstance<T> RegisterSelfAsSingleton<T>(this IInitializationExpression expression) where T : class
        {
            return expression.ForSingletonOf<T>().Use<T>();
        }

        public static SmartInstance<T> RegisterSelfAsSingleton<T>(this IRegistry registry)
        {
            return registry.ForSingletonOf<T>().Use<T>();
        }

        public static void ScanThisAssembly(this IRegistry registry)
        {
            registry.Scan(x =>
            {
                x.TheCallingAssembly();
                x.WithDefaultConventions();
            });
        }

        public static void ScanNamespaceContaining<T>(this IRegistry registry)
        {
            registry.Scan(x =>
            {
                x.TheCallingAssembly();
                x.IncludeNamespaceContainingType<T>();
                x.WithDefaultConventions();
            });
        }

        public static void RegisterFirstInterfacesInNamespaceContaining<T>(this IRegistry registry)
        {
            registry.Scan(x =>
            {
                x.AssemblyContainingType<T>();
                x.IncludeNamespaceContainingType<T>();
                x.RegisterConcreteTypesAgainstTheFirstInterface();
            });
        }

        public static void StartServices(this IContainer container)
        {
            container.Model.GetAllPossible<IStartable>().ForEach(x => x.Start());
        }
    }
}