using Teledoc.Domain.Infrastructure.Enums;
using Teledoc.Domain.Interfaces;

namespace Teledoc.Domain.Models.Base
{
    public abstract class Client : Entity, IHasIncorporators
    {
        public string Inn { get; private set; }
        public string Name { get; set; }
        public ClientType ClientType { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime UpdateDate { get; set; }

        protected Client(string inn, ClientType clientType, string name)
        {
            Inn = inn;
            ClientType = clientType;
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
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

        /// <summary>
        /// Необходим для получения списка учредителей
        /// </summary>
        /// <returns>У ИП IEnumerable с 1 элементом, у Физ лиц IEnumberable их учредителей</returns>
        public abstract IEnumerable<Incorporator> GetIncorporators();
    }
}
