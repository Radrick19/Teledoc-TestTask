using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Domain.Interfaces;
using Teledoc.Domain.Models.Base;

namespace Teledoc.Database.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : Entity
    {
        protected readonly TeledocContext _context;
        protected readonly DbSet<T> dbSet;

        public BaseRepository(TeledocContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        public virtual async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public virtual async Task DeleteByIdAsync(int entityId)
        {
            T entity = await dbSet.FindAsync(entityId);
            dbSet.Remove(entity);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>>? expression = null)
        {
            IQueryable<T> query = dbSet;
            if(expression != null)
                query = query.Where(expression);
            return query.AsEnumerable();
        }

        public virtual void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await dbSet.FirstOrDefaultAsync(expression);
        }
    }
}
