using Microsoft.AspNetCore.Mvc;
using Teledoc.Application.Interfaces;
using Teledoc.Database;
using Teledoc.Domain.Interfaces;
using Teledoc.Domain.Models;
using TeledocApp.ViewModels;

namespace TeledocApp.Controllers
{
    public class FounderController : Controller
    {
        private readonly IFounderService _founderService;
        private readonly TeledocContext _context;

        public FounderController(TeledocContext context, IFounderService founderService)
        {
            _context = context;
            _founderService = founderService;
        }

        [HttpGet]
        public IActionResult Index() 
        { 
            return View(); 
        }

        [HttpGet]
        public IActionResult AddFounder()
        {
            return View(new AddFounderForm());
        }

        /// <summary>
        /// Добавление новых учредителей
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddFounder(AddFounderForm viewModel)
        {
            if(_founderService.FounderWithInnExist(viewModel.Inn))
                ModelState.AddModelError("Inn", "Данный ИНН уже зарегистрирован");

            if(viewModel.Inn.Length != 12)
                ModelState.AddModelError("Inn", "ИНН должен состоять из 12 цифр");

            if (!ModelState.IsValid)
                return View(viewModel);

            var founder = new Founder(viewModel.Inn, viewModel.FullName);
            await _founderService.AddFounder(founder);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Таблица с данными всех учредителей для запросов из JS
        /// </summary>
        /// <returns></returns>
        [HttpGet("get/founderstable")]
        public IActionResult FoundersTable()
        {
            var founders = _founderService.GetAllFounders();
            return PartialView(founders);
        }
    }
}
