using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Application.Interfaces;
using Teledoc.Database;
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
            _founderRepository = founderRepository;
            _context = context;
            _clientFounderRepository = clientFounderRepository;
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
            return _clientFounderRepository.GetQuary().Any(cf=> cf.Founder.Inn == inn && cf.Client.ClientType == ClientType.IndividualPerson);
        }

        public bool FounderWithInnExist(string inn)
        {
            return _founderRepository.GetQuary().Any(founder => founder.Inn == inn);
        }

        public IEnumerable<Founder> GetAllFounders()
        {
            return _founderRepository.GetQuary();
        }

        public Founder? GetFounderByInn(string inn)
        {
            return _founderRepository.GetQuary().FirstOrDefault(founder => founder.Inn == inn);
        }

        public async Task RemoveFounder(string inn)
        {
            var founderForDelete = _founderRepository.GetQuary().FirstOrDefault(founder => founder.Inn == inn);
            _founderRepository.Delete(founderForDelete);
            await _context.SaveChangesAsync();
        }
    }
}
