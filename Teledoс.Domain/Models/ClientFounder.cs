using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Domain.Models.Base;

namespace Teledoc.Domain.Models
{
    public class ClientFounder : Entity
    {
        [NotMapped]
        public override int Id { get => base.Id; set => base.Id = value; }

        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public Client Client { get; set; }

        public int FounderId { get; set; }

        [ForeignKey(nameof(FounderId))]  
        public Founder Founder { get; set; }

        public ClientFounder()
        {
            
        }
    }
}
