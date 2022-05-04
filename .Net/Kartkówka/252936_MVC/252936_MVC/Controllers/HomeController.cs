using _252936_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _252936_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
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

        public IActionResult MovieResult()
        {
            var k = new Movie() { Author = "Robert Makłowicz", Name = "Jak poznałem wasza matke" };
            return View(k);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}