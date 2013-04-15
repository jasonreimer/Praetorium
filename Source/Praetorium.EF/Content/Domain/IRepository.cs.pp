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

using System;
using System.Collections.Generic;
using System.Linq;

namespace $rootnamespace$.Domain
{
    public interface IRepository : IDisposable
    {

        TEntity Get<TEntity>(long id) where TEntity : class, IAggregateRoot;

        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class, IAggregateRoot;

        IQueryable<TEntity> Query<TEntity>() where TEntity : class, IAggregateRoot;

        void Save<TEntity>(TEntity rootEntity) where TEntity : class, IAggregateRoot;

        void Save<TEntity>(IEnumerable<TEntity> rootEntities) where TEntity : class, IAggregateRoot;

        void Delete<TEntity>(TEntity rootEntity) where TEntity : class, IAggregateRoot;

        void Delete<TEntity>(IEnumerable<TEntity> rootEntities) where TEntity : class, IAggregateRoot;

        ///// <summary>
        ///// Completes the session, and commits any pending transactions.
        ///// </summary>
        void Commit();

    }
}
