using Teledoc.Domain.Enums;
using Teledoc.Domain.Interfaces;
using Teledoc.Domain.Models.Base;

namespace Teledoc.Domain.Models
{
    public class Client : Entity
    {
        public string Inn { get; private set; }
        public string Name { get; set; }
        public ClientType ClientType { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime UpdateDate { get; set; }
        public ICollection<ClientFounder> Founders { get; private set; }

        public Client(string inn, ClientType clientType, string name)
        {
            Inn = inn;
            ClientType = clientType;
            var dateTimeNow = DateTime.Now;
            CreateDate = dateTimeNow;
            UpdateDate = dateTimeNow;
            Name = name;
        }

        protected Client()
        {

        }

        // Инкапсулированные методы для будущего расширения

        public void UpdateInn(string newInn)
        {
            Inn = newInn;
            UpdateDate = DateTime.Now;
        }
    }
}
