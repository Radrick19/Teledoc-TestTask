using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledok.Domain.Infrastructure.Enums;
using Teledok.Domain.Models.Base;
using Teledok.Domain.Models.Clients;

namespace Teledok.Domain.Models
{
    /// <summary>
    /// Учредитель
    /// </summary>
    public class Incorporator : Entity
    {
        public string Inn { get; set; }
        public string FullName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public ICollection<LegalEntityIncorporator>? LegalEntities { get; set; }

        public Incorporator(string inn, string fullName)
        {
            Inn = inn;
            FullName = fullName;
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
        }

        protected Incorporator()
        {
            
        }

        // Инкапсулированные методы для будущего расширения

        public void UpdateInn(string newInn)
        {
            Inn = newInn;
            UpdateDate = DateTime.Now;
        }

        public void UpdateName(string newFullName)
        {
            FullName = newFullName;
            UpdateDate = DateTime.Now;
        }
    }
}
