using System.ComponentModel.DataAnnotations;

namespace TeledocApp.ViewModels
{
    public class AddClientViewModel
    {
        [RegularExpression("^[0-9]+$", ErrorMessage = "ИНН должен состоять только из арабских цифр")]
        public string Inn { get; set; }
        public string Name { get; set; }
        public string ClientType { get; set; }

        [RegularExpression("^$|^[0-9X]{12}$", ErrorMessage = "ИНН должен состоять только из 12 арабских цифр")]
        public string IncorporatorInn { get; set; }
    }
}
