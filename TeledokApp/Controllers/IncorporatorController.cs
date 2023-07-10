using Microsoft.AspNetCore.Mvc;
using Teledok.Domain.Interfaces;
using Teledok.Domain.Models;
using TeledokApp.ViewModels;

namespace TeledokApp.Controllers
{
    public class IncorporatorController : Controller
    {
        private readonly IRepository<Incorporator> _incorporatorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public IncorporatorController(IRepository<Incorporator> incorporatorRepository, IUnitOfWork unitOfWork)
        {
            _incorporatorRepository = incorporatorRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index() 
        { 
            return View(); 
        }

        [HttpGet]
        public IActionResult AddIncorporator()
        {
            return View(new AddIncorporatorViewModel());
        }

        /// <summary>
        /// Добавление новых учредителей
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddIncorporator(AddIncorporatorViewModel viewModel)
        {
            if(_incorporatorRepository.GetQuary().Any(inc=> inc.Inn == viewModel.Inn))
                ModelState.AddModelError("Inn", "Данный ИНН уже зарегистрирован");

            if (!ModelState.IsValid)
                return View(viewModel);

            var incorporator = new Incorporator(viewModel.Inn, viewModel.FullName);
            await _incorporatorRepository.AddAsync(incorporator);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Таблица с данными всех учредителей для запросов из JS
        /// </summary>
        /// <returns></returns>
        [HttpGet("get/incorporatorstable")]
        public IActionResult IncorporatorsTable()
        {
            var incorporators = _incorporatorRepository.GetQuary().AsEnumerable();
            var viewModel = new IncorporatorTableViewModel(incorporators);
            return PartialView(viewModel);
        }
    }
}
