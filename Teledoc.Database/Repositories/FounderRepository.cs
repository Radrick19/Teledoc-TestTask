using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public override IEnumerable<Founder> Get(Expression<Func<Founder, bool>>? expression = null)
        {
            IQueryable<Founder> query = dbSet.Include(inc => inc.Clients);
            if (expression != null)
                query = query.Where(expression);
            return query.AsEnumerable();
        }

        public override Task<Founder> GetAsync(int id)
        {
            return dbSet.Include(inc => inc.Clients).FirstAsync(inc=> inc.Id == id);
        }
    }
}
