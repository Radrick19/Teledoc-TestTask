using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Domain.Infrastructure.Enums;
using Teledoc.Domain.Models.Base;

namespace Teledoc.Domain.Models.Clients
{
    /// <summary>
    /// Индивидуальный предприниматель
    /// </summary>
    public class IndividualPerson : Client
    {
        public int IncorporatorId { get; set; }

        [ForeignKey(nameof(IncorporatorId))]
        public Incorporator Incorporator { get; set; }

        public IndividualPerson(string inn, string name, Incorporator incorporator) :
            base(inn, ClientType.IndividualPerson, name)
        {
            Incorporator = incorporator;
        }

        protected IndividualPerson()
        {
            
        }

        public override IEnumerable<Incorporator> GetIncorporators()
        {
            var incorporatorList = new List<Incorporator>
            {
                Incorporator
            };
            return incorporatorList;
        }
    }
}
