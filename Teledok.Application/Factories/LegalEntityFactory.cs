using System.Text.RegularExpressions;
using Teledok.Domain.Models.Base;
using Teledok.Domain.Models.Clients;

namespace Teledok.Domain.Infrastructure.Factories
{
    public class LegalEntityFactory
    {
        public static LegalEntity Create(string innNumbers, string name)
        {
            if (innNumbers.Length != 10)
                throw new ArgumentException("Inn of legal entities must be 10 characters");

            if (!Regex.IsMatch(innNumbers, "^[0-9]+$"))
                throw new ArgumentException("Inn must consist only of arabic numerals");

            return new LegalEntity(innNumbers, name);
        }
    }
}
