using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teledoc.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        TeledokContext GetContext();

        Task BeginTransaction();

        Task CommitTransaction();

        Task RollbackTransaction();

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
