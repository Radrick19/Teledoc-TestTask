using Teledok.Domain.Models.Base;

namespace TeledokApp.ViewModels
{
    public class ClientTableViewModel
    {
        public IEnumerable<Client> Clients;

        public ClientTableViewModel(IEnumerable<Client> clients)
        {
            Clients = clients;
        }
    }
}
