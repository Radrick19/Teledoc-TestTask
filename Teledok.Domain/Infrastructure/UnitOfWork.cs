using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Domain.Interfaces;

namespace Teledoc.Domain.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private TeledokContext dbContext;

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        private bool disposed = false;

        public TeledokContext GetContext()
        {
            if (disposed)
            {
                dbContext = new TeledokContext();
            }
            return dbContext;
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task BeginTransaction()
        {
            await dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            await dbContext.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransaction()
        {
            await dbContext.Database.RollbackTransactionAsync();
        }

        public UnitOfWork(TeledokContext context)
        {
            dbContext = context;
        }
    }
}
