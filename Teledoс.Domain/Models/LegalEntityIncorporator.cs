using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Domain.Models.Base;
using Teledoc.Domain.Models.Clients;

namespace Teledoc.Domain.Models
{
    public class LegalEntityIncorporator : Entity
    {
        [NotMapped]
        public override int Id { get => base.Id; set => base.Id = value; }

        public int LegalEntityId { get; set; }

        [ForeignKey(nameof(LegalEntityId))]
        public LegalEntity LegalEntity { get; set; }

        public int IncorporatorId { get; set; }

        [ForeignKey(nameof(IncorporatorId))]  
        public Incorporator Incorporator { get; set; }

        public LegalEntityIncorporator()
        {
            
        }
    }
}
