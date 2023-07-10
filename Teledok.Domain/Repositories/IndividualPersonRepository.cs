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
    public class IndividualPersonRepository : BaseRepository<IndividualPerson>
    {
        public IndividualPersonRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override IQueryable<IndividualPerson> GetQuary()
        {
            return dbSet.Include(ip => ip.Incorporator);
        }

        public override async Task<IndividualPerson> GetAsync(int id)
        {
            return await dbSet.Include(ip => ip.Incorporator).FirstAsync(ip=> ip.Id == id);
        }
    }
}
