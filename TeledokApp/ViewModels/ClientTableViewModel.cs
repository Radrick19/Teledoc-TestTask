using Teledoc.Domain.Models.Base;

namespace TeledocApp.ViewModels
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
