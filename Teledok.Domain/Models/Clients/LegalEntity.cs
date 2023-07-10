using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledok.Domain.Infrastructure.Enums;
using Teledok.Domain.Models.Base;

namespace Teledok.Domain.Models.Clients
{
    /// <summary>
    /// Юридическое лицо
    /// </summary>
    public class LegalEntity : Client
    {
        public ICollection<LegalEntityIncorporator> Incorporators;

        public LegalEntity(string inn, string name) :
            base(inn, ClientType.LegalEntity, name)
        {
        }

        protected LegalEntity()
        {
            
        }

        public override IEnumerable<Incorporator> GetIncorporators()
        {
            return Incorporators.Select(lei=> lei.Incorporator).AsEnumerable();
        }
    }
}
