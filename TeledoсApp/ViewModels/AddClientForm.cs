using System.ComponentModel.DataAnnotations;

namespace TeledocApp.ViewModels
{
    public class AddClientForm
    {
        [RegularExpression(@"^\d$", ErrorMessage ="ИНН должен состоять только из цифр")]
        public string Inn { get; set; }
        public string Name { get; set; }
        public string ClientType { get; set; }
        public string FounderInn { get; set; }
    }
}
