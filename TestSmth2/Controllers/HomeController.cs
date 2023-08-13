using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestSmth2.Models;
using TestSmth2.Models.ViewModels;
using TestSmth2.Repos;

namespace TestSmth2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEntryRepo entryRepo;
        private readonly IAntiBioticRepo antiBioticRepo;

        public HomeController(ILogger<HomeController> logger, IEntryRepo entryRepo, IAntiBioticRepo antiBioticRepo)
        {
            _logger = logger;
            this.entryRepo = entryRepo;
            this.antiBioticRepo = antiBioticRepo;
        }
        [HttpGet]
        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            var entries = await entryRepo.GetAllAsync();
            var antibiotics = await antiBioticRepo.GetAllAsync();
            var model = new HomeViewModel
            {
                entries = entries,
                antibiotics = antibiotics
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
