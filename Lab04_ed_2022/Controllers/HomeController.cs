using ClassLibrary;
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
            QueuePriority<int> prueba = new QueuePriority<int>((int n) => n, (PriorityNode<int> n1, PriorityNode<int> n2) => n1.Priority > n2.Priority);
            prueba.Insert(7);
            prueba.Insert(3);
            prueba.Insert(19);
            prueba.Insert(8);
            prueba.Insert(10);
            prueba.Insert(2);
            prueba.Insert(4);
            prueba.Insert(20);
            prueba.Insert(1);
            prueba.Insert(7);

            prueba.Remove();
            prueba.Remove();
            prueba.Remove();
            prueba.Remove();
            prueba.Remove();
            prueba.Remove();
            prueba.Remove();
            prueba.Remove();
            prueba.Remove();
            prueba.Remove();
            prueba.Remove();
            prueba.Remove();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
