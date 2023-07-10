using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledok.Domain.Interfaces;
using Teledok.Domain.Models.Clients;

namespace Teledok.Domain.Repositories
{
    public class LegalEntityRepository : BaseRepository<LegalEntity>
    {
        public LegalEntityRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override IQueryable<LegalEntity> GetQuary()
        {
            return dbSet.Include(le => le.Incorporators).ThenInclude(inc=> inc.Incorporator);
        }

        public override Task<LegalEntity> GetAsync(int id)
        {
            return dbSet.Include(le => le.Incorporators).ThenInclude(inc => inc.Incorporator).FirstAsync(le=> le.Id == id);
        }
    }
}
