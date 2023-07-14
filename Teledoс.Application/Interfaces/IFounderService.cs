using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Domain.Models;

namespace Teledoc.Application.Interfaces
{
    public interface IFounderService
    {
        Task<Founder?> GetFounderByInn(string inn);

        IEnumerable<Founder> GetAllFounders();

        Task AddFounder(Founder founder);

        Task RemoveFounder(string inn);

        Task AddFounderToClient(Client client, Founder founder);

        bool FounderWithInnExist(string inn);

        bool FounderHasIndividualPerson(string inn);
    }
}
