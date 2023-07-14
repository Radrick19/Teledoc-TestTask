using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Teledoc.Domain.Models;

namespace Teledoc.Database.Repositories
{
    public class ClientRepository : BaseRepository<Client>
    {
        public ClientRepository(TeledocContext context) : base(context)
        {
        }

        public override IEnumerable<Client> Get(Expression<Func<Client, bool>>? expression = null)
        {
            IQueryable<Client> query = dbSet.Include(le => le.Founders).ThenInclude(inc => inc.Founder);
            if (expression != null)
                query = query.Where(expression);
            return query.AsEnumerable();
        }

        public override Task<Client> GetAsync(int id)
        {
            return dbSet.Include(le => le.Founders).ThenInclude(inc => inc.Founder).FirstAsync(le=> le.Id == id);
        }

        public async override Task<Client> FirstOrDefaultAsync(Expression<Func<Client, bool>> expression)
        {
            return await dbSet.Include(le => le.Founders).ThenInclude(inc => inc.Founder).FirstOrDefaultAsync(expression);
        }
    }
}
