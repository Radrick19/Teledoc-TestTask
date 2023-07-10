using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Teledok.Domain.Infrastructure.Enums;
using Teledok.Domain.Infrastructure.Factories;
using Teledok.Domain.Interfaces;
using Teledok.Domain.Models;
using Teledok.Domain.Models.Base;
using Teledok.Domain.Models.Clients;
using TeledokApp.ViewModels;

namespace TeledokApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IRepository<IndividualPerson> _individualPersonRepository;
        private readonly IRepository<LegalEntity> _legalEntityRepository;
        private readonly IRepository<Incorporator> _incorporatorRepository;
        private readonly IRepository<LegalEntityIncorporator> _legalEntityIncorporatorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClientController(IRepository<IndividualPerson> individualPersonRepository,
            IRepository<LegalEntity> legalEntityRepository, IUnitOfWork unitOfWork, IRepository<Incorporator> incorporatorRepository, IRepository<LegalEntityIncorporator> legalEntityIncorporatorRepository)
        {
            _individualPersonRepository = individualPersonRepository;
            _legalEntityRepository = legalEntityRepository;
            _unitOfWork = unitOfWork;
            _incorporatorRepository = incorporatorRepository;
            _legalEntityIncorporatorRepository = legalEntityIncorporatorRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Добавляет физ лицу нового учредителя
        /// </summary>
        /// <param name="incorporatorInn">ИНН учредителя</param>
        /// <param name="clientInn">ИНН физ лица</param>
        [HttpPost]
        public async Task<IActionResult> AddIncorporator(string incorporatorInn, string clientInn)
        {
            var client = _legalEntityRepository.GetQuary().FirstOrDefault(le=> le.Inn == clientInn);
            var incorporator = _incorporatorRepository.GetQuary().FirstOrDefault(inc=> incorporatorInn == inc.Inn);

            if(incorporator == null)
                ModelState.AddModelError("incorporatorInn", "Данный ИНН не зарегистрирован");

            if (client.Incorporators.Any(inc=> incorporatorInn == inc.Incorporator.Inn))
                ModelState.AddModelError("incorporatorInn", "Данный ИНН уже добавлен");

            if (!ModelState.IsValid)
                return View("ClientDetails", client);

            LegalEntityIncorporator legalEntityIncorporator = new LegalEntityIncorporator()
            {
                Incorporator = incorporator,
                LegalEntity = client
            };

            client.UpdateDate = DateTime.Now;
            _legalEntityRepository.Update(client);
            await _legalEntityIncorporatorRepository.AddAsync(legalEntityIncorporator);
            await _unitOfWork.SaveChangesAsync();
            return View("ClientDetails", client);
        }

        /// <summary>
        /// Вся информация по клиенту
        /// </summary>
        /// <param name="clientId">Id клиента</param>
        /// <param name="clientType">Enum типа клиента</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ClientDetails(int clientId, ClientType clientType)
        {
            if(clientType == ClientType.IndividualPerson)
                return View(await _individualPersonRepository.GetAsync(clientId));

            else
                return View(await _legalEntityRepository.GetAsync(clientId));
        }

        [HttpGet]
        public IActionResult AddClient()
        {
            return View(new AddClientViewModel());
        }

        /// <summary>
        /// Добавление нового клиента
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddClient(AddClientViewModel viewModel)
        {
            var enumType = (ClientType)Enum.Parse(typeof(ClientType), viewModel.ClientType);
            var incorporator = _incorporatorRepository.GetQuary().FirstOrDefault(inc => inc.Inn == viewModel.IncorporatorInn);
            if (incorporator == null)
                ModelState.AddModelError("IncorporatorInn", "ИНН учредителя нет в базе");

            if (enumType == ClientType.IndividualPerson)
            {
                if(viewModel.Inn.Length != 12)
                    ModelState.AddModelError("Inn", "ИНН ИП должен состоять из 12 цифр, а физ лица из 10");

                if (_individualPersonRepository.GetQuary().Any(ip => viewModel.Inn == ip.Inn))
                    ModelState.AddModelError("Inn", "Данный ИНН уже занят");

                if (_individualPersonRepository.GetQuary().Any(ip => ip.IncorporatorId == incorporator.Id))
                    ModelState.AddModelError("IncorporatorInn", "У данного учредителя уже есть ИП");

                if (!ModelState.IsValid)
                    return View(viewModel);

                IndividualPerson ip = IndividualPersonFactory.Create(viewModel.Inn, viewModel.Name, incorporator);
                await _individualPersonRepository.AddAsync(ip);
            }
            else
            {
                if (viewModel.Inn.Length != 10)
                    ModelState.AddModelError("Inn", "ИНН ИП должен состоять из 12 цифр, а физ лица из 10");

                if (_legalEntityRepository.GetQuary().Any(ip => viewModel.Inn == ip.Inn))
                    ModelState.AddModelError("Inn", "Данный ИНН уже занят");

                if (_legalEntityRepository.GetQuary().Any(ip => viewModel.Name == ip.Name))
                    ModelState.AddModelError("Name", "Данное название уже занято");

                if(!ModelState.IsValid)
                    return View(viewModel);

                LegalEntity le = LegalEntityFactory.Create(viewModel.Inn, viewModel.Name);
                LegalEntityIncorporator legalEntityIncorporator = new LegalEntityIncorporator()
                {
                    Incorporator = incorporator,
                    LegalEntity = le
                };
                await _legalEntityIncorporatorRepository.AddAsync(legalEntityIncorporator);
            }

            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        /// <summary>
        /// Таблица с данными всех ИП для запросов из JS
        /// </summary>
        /// <returns></returns>
        [HttpGet("get/iptable")]
        public IActionResult IndividualPersonsTable()
        {
            var individualPersons = _individualPersonRepository.GetQuary().AsEnumerable();
            var viewModel = new ClientTableViewModel(individualPersons);
            return PartialView("ClientTable", viewModel);
        }

        /// <summary>
        /// Таблица с данными всех физ лиц для запросов из JS
        /// </summary>
        /// <returns></returns>
        [HttpGet("get/letable")]
        public IActionResult LegalEntitiesTable()
        {
            var legalEntities = _legalEntityRepository.GetQuary();
            var viewModel = new ClientTableViewModel(legalEntities);
            return PartialView("ClientTable", viewModel);
        }
    }
}