using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Application.Interfaces;
using Teledoc.Database;
using Teledoc.Database.Repositories;
using Teledoc.Domain.Enums;
using Teledoc.Domain.Interfaces;
using Teledoc.Domain.Models;

namespace Teledoc.Application.Services
{
    public class FounderService : IFounderService
    {
        private readonly IRepository<Founder> _founderRepository;
        private readonly IRepository<ClientFounder> _clientFounderRepository;
        private readonly TeledocContext _context;

        public FounderService(IRepository<Founder> founderRepository, TeledocContext context, IRepository<ClientFounder> clientFounderRepository)
        {
            _founderRepository = founderRepository ?? throw new ArgumentNullException(nameof(founderRepository));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _clientFounderRepository = clientFounderRepository ?? throw new ArgumentNullException(nameof(clientFounderRepository));
        }

        public async Task AddFounder(Founder founder)
        {
            await _founderRepository.AddAsync(founder);
            await _context.SaveChangesAsync();
        }

        public async Task AddFounderToClient(Client client, Founder founder)
        {
            var clientFounder = new ClientFounder() { Client = client, Founder = founder };
            client.UpdateDate = DateTime.Now;
            await _clientFounderRepository.AddAsync(clientFounder);
            await _context.SaveChangesAsync();
        }

        public bool FounderHasIndividualPerson(string inn)
        {
            return _clientFounderRepository.Get(cf => cf.Founder.Inn == inn && cf.Client.ClientType == ClientType.IndividualPerson).Any();
        }

        public bool FounderWithInnExist(string inn)
        {
            return _founderRepository.Get(founder => founder.Inn == inn).Any();
        }

        public IEnumerable<Founder> GetAllFounders()
        {
            return _founderRepository.Get();
        }

        public async Task<Founder?> GetFounderByInn(string inn)
        {
            return await _founderRepository.FirstOrDefaultAsync(founder => founder.Inn == inn);
        }

        public async Task RemoveFounder(string inn)
        {
            var founderForDelete = _founderRepository.Get().FirstOrDefault(founder => founder.Inn == inn);
            if (founderForDelete == null) { throw new ArgumentException(); }
            _founderRepository.Delete(founderForDelete);
            await _context.SaveChangesAsync();
        }
    }
}
