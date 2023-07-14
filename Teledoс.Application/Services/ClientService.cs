using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Application.Interfaces;
using Teledoc.Database;
using Teledoc.Domain.Enums;
using Teledoc.Domain.Interfaces;
using Teledoc.Domain.Models;

namespace Teledoc.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IRepository<Client> _clientRepository;
        private readonly TeledocContext _context;

        public ClientService(IRepository<Client> clientRepository, TeledocContext context)
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddClient(Client client)
        {;
            await _clientRepository.AddAsync(client);
            await _context.SaveChangesAsync();
        }

        public bool ClientWithInnExist(string? inn)
        {
            if(inn == null) return false; 
            return _clientRepository.Get(client => client.Inn == inn).Any();
        }

        public bool ClientWithNameExist(string name)
        {
            return _clientRepository.Get(client => client.Name == name).Any();
        }

        public async Task<Client?> GetClientByInn(string inn)
        {
            return await _clientRepository.FirstOrDefaultAsync(client=> client.Inn == inn);
        }

        public IEnumerable<Client> GetClients(ClientType? clientType = null)
        {
            if(clientType == null)
            {
                return _clientRepository.Get();
            }
            return _clientRepository.Get(client => client.ClientType == clientType);
        }

        public async Task RemoveClient(string inn)
        {
            var clientForDelete = await _clientRepository
                .FirstOrDefaultAsync(client => client.Inn == inn);

            if (clientForDelete == null) throw new ArgumentException();

            _clientRepository.Delete(clientForDelete);
            await _context.SaveChangesAsync();
        }
    }
}
