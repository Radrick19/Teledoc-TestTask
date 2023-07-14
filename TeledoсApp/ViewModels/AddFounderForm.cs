using System.ComponentModel.DataAnnotations;

namespace TeledocApp.ViewModels
{
    public class AddFounderForm
    {
        [RegularExpression(@"^\d$", ErrorMessage = "ИНН должен состоять только из цифр")]
        public string Inn { get; set; }

        public string FullName { get; set; }
    }
}
