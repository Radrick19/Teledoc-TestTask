using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Teledoc.Domain.Models;
using Teledoc.Domain.Models.Clients;

namespace Teledoc.Domain.Infrastructure.Factories
{
    public class IndividualPersonFactory
    {
        // Так как clients это ключевые сущности приложения стоит инкапсулировать их создание в фабрики, для возоможности расширения в будущем
        public static IndividualPerson Create(string innNumbers, string name, Incorporator incorporator)
        {
            if (innNumbers.Length != 12)
                throw new ArgumentException("Inn of individuals must be 12 characters");

            if (!Regex.IsMatch(innNumbers, "^[0-9]+$"))
                throw new ArgumentException("Inn must consist only of arabic numerals");

            return new IndividualPerson(innNumbers, name, incorporator);
        }
    }
}
