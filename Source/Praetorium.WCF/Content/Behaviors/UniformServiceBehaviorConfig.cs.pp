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

using Praetorium.Services;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace $rootnamespace$.Behaviors
{
    public class UniformServiceBehaviorConfig
    {
        public static readonly Type[] ServiceBehaviorTypes = new[] 
        {
            typeof(ErrorHandlerBehavior),
            typeof(ServiceFactoryBehavior),
        };

        public static readonly Func<IServiceBehavior>[] ServiceBehaviorFactories = new Func<IServiceBehavior>[]
        {
                CreateServiceBehavior,
#if !RELEASE
                CreateServiceMetadataBehavior,
#endif
        };

        public static IServiceBehavior CreateServiceBehavior()
        {
#if !RELEASE
            const bool includeExceptionDetails = false;
#else
            const bool includeExceptionDetails = true;
#endif

            var behavior = new ServiceBehaviorAttribute
            {
                ConcurrencyMode = ConcurrencyMode.Multiple,
                InstanceContextMode = InstanceContextMode.Single,
                MaxItemsInObjectGraph = Int32.MaxValue, // adjust as necessary
                Namespace = ServiceNamespaces.Root,
                IncludeExceptionDetailInFaults = includeExceptionDetails,
            };

            return behavior;
        }

#if !RELEASE
        public static IServiceBehavior CreateServiceMetadataBehavior()
        {
            var behavior = new ServiceMetadataBehavior
            {
                HttpGetEnabled = true,
                HttpsGetEnabled = true,
            };

            return behavior;
        }
#endif
    }
}
