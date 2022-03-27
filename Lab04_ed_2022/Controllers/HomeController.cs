using Lab04_ed_2022.Helpers;
using Lab04_ed_2022.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Lab04_ed_2022.Controllers
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

            Data.Instance.Prueba.Insert(15);
            Data.Instance.Prueba.Insert(3);
            Data.Instance.Prueba.Insert(22);
            Data.Instance.Prueba.Insert(5);
            Data.Instance.Prueba.Insert(10);
            Data.Instance.Prueba.Insert(12);
            Data.Instance.Prueba.Insert(35);
            Data.Instance.Prueba.Insert(90);
            Data.Instance.Prueba.Insert(27);
            Data.Instance.Prueba.Insert(43);
            Data.Instance.Prueba.Insert(67);


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
