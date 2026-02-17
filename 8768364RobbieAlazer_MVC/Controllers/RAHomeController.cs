using _8768364RobbieAlazer_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _8768364RobbieAlazer_MVC.Controllers
{
    public class RAHomeController : Controller
    {
        private readonly ILogger<RAHomeController> _logger;

        public RAHomeController(ILogger<RAHomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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
