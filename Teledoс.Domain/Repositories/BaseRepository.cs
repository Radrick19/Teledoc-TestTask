using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Domain.Interfaces;
using Teledoc.Domain.Models.Base;

namespace Teledoc.Domain.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : Entity
    {
        protected TeledokContext context => _unitOfWork.GetContext();
        protected readonly DbSet<T> dbSet;
        protected readonly IUnitOfWork _unitOfWork;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            dbSet = context.Set<T>();
        }

        public virtual async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public virtual async Task DeleteAsync(int entityId)
        {
            T entity = await dbSet.FindAsync(entityId);
            dbSet.Remove(entity);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual IQueryable<T> GetQuary()
        {
            return dbSet;
        }

        public virtual void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await dbSet.FirstOrDefaultAsync(expression);
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return dbSet.FirstOrDefault(expression);
        }
    }
}
