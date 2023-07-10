﻿using System.ComponentModel.DataAnnotations;

namespace TeledokApp.ViewModels
{
    public class AddIncorporatorViewModel
    {
        [RegularExpression("^$|^[0-9X]{12}$", ErrorMessage = "ИНН должен состоять только из 12 арабских цифр")]
        public string Inn { get; set; }

        public string FullName { get; set; }
    }
}