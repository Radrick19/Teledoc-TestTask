using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Domain.Enums;
using Teledoc.Domain.Models;

namespace Teledoc.Application.Interfaces
{
    public interface IClientService
    {
        Task<Client?> GetClientByInn(string inn);

        IEnumerable<Client> GetClients(ClientType? clientType = null);

        Task AddClient(Client client);

        Task RemoveClient(string inn);
        
        bool ClientWithInnExist(string inn);

        bool ClientWithNameExist(string name);
    }
}
