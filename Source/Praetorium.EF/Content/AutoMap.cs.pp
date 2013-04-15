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
using System;
using System.Data.Entity;
using System.Linq;

namespace $rootnamespace$
{
    public static class AutoMap<T> where T : AutoMappingConfiguration, new()
    {
        private static readonly Type _mappingOverrideType = typeof(IMappingOverride<>);
        private static readonly AutoMappingConfiguration _autoMappingConfiguration = new T();

        public static void Using(DbModelBuilder modelBuilder) 
        {
            ConfigureModels(modelBuilder);
        }

        private static void ConfigureModels(DbModelBuilder modelBuilder)
        {
            var entityMethod = typeof(DbModelBuilder).GetMethod("Entity");
            var entityTypes = _autoMappingConfiguration.GetEntityTypes();

            if (entityTypes.Count() > 0)
            {
                var mappingTypes = _autoMappingConfiguration.OverridesInThisAssembly();

                mappingTypes.ForEach(x => Console.WriteLine(x.FullName));

                foreach (var entityType in entityTypes)
                {
                    var entityDef = entityMethod.MakeGenericMethod(entityType).Invoke(modelBuilder, new object[] { });

                    mappingTypes
                        .Where(x => ForEntity(x, entityType))
                        .ForEach(x => InvokeConfigure(x.Create(), entityDef));
                }
            }
        }

        private static void InvokeConfigure(object mapping, object entityDef)
        {
            var configureMethod = mapping.GetType().GetMethod("Override");

            configureMethod.Invoke(mapping, new[] { entityDef });
        }

        private static bool ForEntity(Type mappingType, Type entityType)
        {
            var overrideType = _mappingOverrideType.MakeGenericType(entityType);

            return mappingType.DerivesFrom(overrideType);
        }
    }
}