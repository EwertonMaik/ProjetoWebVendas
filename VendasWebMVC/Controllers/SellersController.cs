using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VendasWebMVC.Services;

namespace VendasWebMVC.Controllers
{
    public class SellersController : Controller
    {
        //Decladando dependência de SellerService
        private readonly SellerService _sellerService;

        //Definindo Contrutor para injetar Dependência
        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll(); //Retorna um Lista de Vendedores
            return View(list);
        }
    }
}