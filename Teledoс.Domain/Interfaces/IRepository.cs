﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Domain.Models.Base;

namespace Teledoc.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? expression = null);
        public Task<TEntity> GetAsync(int id);
        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> func);
        public Task AddAsync(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
        public Task DeleteByIdAsync(int entityId);
    }
}
