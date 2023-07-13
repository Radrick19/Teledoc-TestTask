using System.ComponentModel.DataAnnotations;

namespace TeledocApp.ViewModels
{
    public class AddClientForm
    {
        public string Inn { get; set; }
        public string Name { get; set; }
        public string ClientType { get; set; }
        public string FounderInn { get; set; }
    }
}
