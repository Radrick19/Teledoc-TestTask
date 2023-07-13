using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Domain.Models;

namespace Teledoc.Database.Repositories
{
    public class FounderRepository : BaseRepository<Founder>
    {
        public FounderRepository(TeledocContext context) : base(context)
        {
        }

        public override IQueryable<Founder> GetQuary()
        {
            return dbSet.Include(inc => inc.Clients);
        }

        public override Task<Founder> GetAsync(int id)
        {
            return dbSet.Include(inc => inc.Clients).FirstAsync(inc=> inc.Id == id);
        }
    }
}
