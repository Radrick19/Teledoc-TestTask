using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledok.Domain.Interfaces;
using Teledok.Domain.Models;

namespace Teledok.Domain.Repositories
{
    public class IncorporatorRepository : BaseRepository<Incorporator>
    {
        public IncorporatorRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override IQueryable<Incorporator> GetQuary()
        {
            return dbSet.Include(inc => inc.LegalEntities);
        }

        public override Task<Incorporator> GetAsync(int id)
        {
            return dbSet.Include(inc => inc.LegalEntities).FirstAsync(inc=> inc.Id == id);
        }
    }
}
