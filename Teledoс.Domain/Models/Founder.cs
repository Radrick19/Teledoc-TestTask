using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teledoc.Domain.Models.Base;

namespace Teledoc.Domain.Models
{
    /// <summary>
    /// Учредитель
    /// </summary>
    public class Founder : Entity
    {
        public string Inn { get; set; }
        public string FullName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public ICollection<ClientFounder>? Clients { get; set; }

        public Founder(string inn, string fullName)
        {
            Inn = inn;
            FullName = fullName;
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
        }

        protected Founder()
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
