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
using $rootnamespace$.Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace $rootnamespace$
{
    public class AppDbContext : DbContext, IRepository, IStartable
    {
        public AppDbContext()
            : base(ConnectionNames.Primary)
        {
        }

        public TEntity Get<TEntity>(long id) where TEntity : class, IAggregateRoot
        {
            return Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class, IAggregateRoot
        {
            return Set<TEntity>().ToArray();
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class, IAggregateRoot
        {
            return Set<TEntity>();
        }

        public void Save<TEntity>(TEntity rootEntity) where TEntity : class, IAggregateRoot
        {
            Ensure.ArgumentNotNull(() => rootEntity);

            if (rootEntity is IValidatableAggregateRoot)
                ((IValidatableAggregateRoot)rootEntity).CanSave();

            Set<TEntity>().Add(rootEntity);
        }

        public void Save<TEntity>(IEnumerable<TEntity> rootEntities) where TEntity : class, IAggregateRoot
        {
            Ensure.ArgumentNotNull(() => rootEntities);

            rootEntities.ForEach(Save);
        }

        public void Delete<TEntity>(TEntity rootEntity) where TEntity : class, IAggregateRoot
        {
            Ensure.ArgumentNotNull(() => rootEntity);

            if (rootEntity is IValidatableAggregateRoot)
                ((IValidatableAggregateRoot)rootEntity).CanDelete();

            Set<TEntity>().Remove(rootEntity);
        }

        public void Delete<TEntity>(IEnumerable<TEntity> rootEntities) where TEntity : class, IAggregateRoot
        {
            Ensure.ArgumentNotNull(() => rootEntities);

            rootEntities.ForEach(Delete);
        }

        public void Commit()
        {
            SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            AutoMap<AutoMappingConfiguration>.Using(modelBuilder);
            
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Configuration>());
        }

        IQueryable<T> IQueryProvider.Query<T>() 
        {
            return Set<T>().AsNoTracking();
        }

        IEnumerable<T> IQueryProvider.SqlQuery<T>(string sql, params object[] parameters)
        {
            return Database.SqlQuery<T>(sql, parameters);
        }

        void IStartable.Start()
        {
            //TODO: if auto-migration at startup is desired, kick off migrations by counting one of the entities here...
        }
    }
}
