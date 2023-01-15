using Microsoft.AspNetCore.Mvc;
using Munamii.Models;
using Munamii.ViewModels;
using System.Diagnostics;

namespace Munamii.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ICakeRepository _cakeRepository;

        public HomeController(ILogger<HomeController> logger, ICakeRepository cakeRepository)
        {
            _logger = logger;
            _cakeRepository = cakeRepository;
        }

        // checks CakesOfTheWeek and adds them to the home page view
        public IActionResult Index()
        {
            var cakesOfTheWeek = _cakeRepository.CakesOfTheWeek;
            var homeViewModel = new HomeViewModel(cakesOfTheWeek);
            return View(homeViewModel);
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