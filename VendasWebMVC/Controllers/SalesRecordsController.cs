using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VendasWebMVC.Controllers
{
    public class SalesRecordsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //Método Busca Simples
        public IActionResult SimpleSearch()
        {
            return View();
        }

        //
        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}