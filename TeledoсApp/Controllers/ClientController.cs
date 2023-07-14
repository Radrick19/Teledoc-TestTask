using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Teledoc.Application.Interfaces;
using Teledoc.Database;
using Teledoc.Database.Repositories;
using Teledoc.Domain.Enums;
using Teledoc.Domain.Interfaces;
using Teledoc.Domain.Models;
using Teledoc.Domain.Models.Base;
using TeledocApp.ViewModels;

namespace TeledocApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IFounderService _founderService;

        public ClientController(IClientService clientService, IFounderService founderService)
        {
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            _founderService = founderService ?? throw new ArgumentNullException(nameof(founderService));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Добавляет физ лицу нового учредителя
        /// </summary>
        /// <param name="founderInn">ИНН учредителя</param>
        /// <param name="clientInn">ИНН физ лица</param>
        [HttpPost]
        public async Task<IActionResult> AddIncorporator(string founderInn, string clientInn)
        {
            var client =await _clientService.GetClientByInn(clientInn);
            var founder = await _founderService.GetFounderByInn(founderInn);

            if(founder == null)
                ModelState.AddModelError("incorporatorInn", "Данный ИНН не зарегистрирован");

            if (client.Founders.Any(fnd=> fnd.Founder == founder))
                ModelState.AddModelError("incorporatorInn", "Данный ИНН уже добавлен");

            if (!ModelState.IsValid)
                return View("ClientDetails", client);

            await _founderService.AddFounderToClient(client, founder);
            return View("ClientDetails", client);
        }

        /// <summary>
        /// Вся информация по клиенту
        /// </summary>
        /// <param name="clientId">Id клиента</param>
        /// <param name="clientType">Enum типа клиента</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ClientDetails(string clientInn)
        {
            return View(await _clientService.GetClientByInn(clientInn));
        }

        [HttpGet]
        public IActionResult AddClient()
        {
            return View(new AddClientForm());
        }

        /// <summary>
        /// Добавление нового клиента
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddClient(AddClientForm viewModel)
        {
            ModelState.Clear();

            var enumType = (ClientType)Enum.Parse(typeof(ClientType), viewModel.ClientType);

            var founder = await _founderService.GetFounderByInn(viewModel.FounderInn);

            if (founder == null)
                ModelState.AddModelError("IncorporatorInn", "ИНН учредителя нет в базе");

            if (_founderService.FounderHasIndividualPerson(founder?.Inn))
                ModelState.AddModelError("IncorporatorInn", "У данного учредителя уже есть ИП");

            if (_clientService.ClientWithInnExist(viewModel.Inn))
                ModelState.AddModelError("Inn", "Данный ИНН уже занят");

            if (_clientService.ClientWithNameExist(viewModel.Name))
                ModelState.AddModelError("Name", "Данное название уже занято");

            if (enumType == ClientType.IndividualPerson)
            {
                if (viewModel.Inn.Length != 12)
                    ModelState.AddModelError("Inn", "ИНН ИП должен состоять из 12 цифр");
            }
            else
            {
                if (viewModel.Inn.Length != 10)
                    ModelState.AddModelError("Inn", "ИНН физ лица должен состоять из 10 цифр");
            }

            if (!ModelState.IsValid)
                return View(viewModel);

            Client client = new Client(viewModel.Inn, enumType, viewModel.Name);
            await _clientService.AddClient(client);
            await _founderService.AddFounderToClient(client, founder);

            return RedirectToAction("Index");
        }


        /// <summary>
        /// Таблица с данными всех ИП для запросов из JS
        /// </summary>
        /// <returns></returns>
        [HttpGet("get/iptable")]
        public IActionResult IndividualPersonsTable()
        {
            var individualPersons = _clientService.GetClients(ClientType.IndividualPerson) ;
            return PartialView("ClientTable", individualPersons);
        }

        /// <summary>
        /// Таблица с данными всех физ лиц для запросов из JS
        /// </summary>
        /// <returns></returns>
        [HttpGet("get/letable")]
        public IActionResult LegalEntitiesTable()
        {
            var legalEntities = _clientService.GetClients(ClientType.LegalEntity);
            return PartialView("ClientTable", legalEntities);
        }
    }
}