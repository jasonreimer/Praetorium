// ----------------------------------------------------------------------------
// Copyright 2013 Jason Reimer
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------

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

		public static void RegisterFirstInterfacesInThisAssembly(this IRegistry registry)
        {
            registry.Scan(x =>
            {
                x.TheCallingAssembly();
                x.RegisterConcreteTypesAgainstTheFirstInterface();
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