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
using Praetorium.Services;
using StructureMap.Configuration.DSL;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using $rootnamespace$.Behaviors;
using $rootnamespace$.FaultBuilders;

namespace $rootnamespace$.DependencyResolution
{
    public class ServicesRegistry : Registry
    {
        public ServicesRegistry()
        {
            IncludeRegistry<PraetoriumRegistry>();
            this.ScanThisAssembly();
            RegisterServiceBehaviors();

            For<IFaultFactory>().Use<FaultFactory>();
            RegisterFaultBuilders();
        }

        private void RegisterFaultBuilders()
        {
            For<IFaultBuilder>().Add<DefaultFaultBuilder>();
        }

        private void RegisterServiceBehaviors()
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.AddAllTypesOf<IServiceBehavior>();
            });

            UniformServiceBehaviorConfig.ServiceBehaviorTypes.ForEach(x => For(typeof(IServiceBehavior)).Add(x));
            UniformServiceBehaviorConfig.ServiceBehaviorFactories.ForEach(x => For<IServiceBehavior>().Add(c => x()));
        }

        private void RegisterDispatchInspector<TInspector>() where TInspector : class, IDispatchMessageInspector
        {
            For<TInspector>().Use<TInspector>();
            For<IServiceBehavior>().Add<DispatchInspectorBehavior<TInspector>>();
        }
    }
}