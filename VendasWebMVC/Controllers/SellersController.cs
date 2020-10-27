using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VendasWebMVC.Models;
using VendasWebMVC.Models.ViewModels;
using VendasWebMVC.Services;

namespace VendasWebMVC.Controllers
{
    public class SellersController : Controller
    {
        //Decladando dependência de SellerService e DepartmentService
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        //Definindo Contrutor para injetar Dependência
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll(); //Retorna um Lista de Vendedores
            return View(list);
        }

        //Ação para criar um vendedor
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        //Método para Excluir Vendedor
        public IActionResult Delete(int? id)
        {
            if (id == null) //Verifica se id recebido no parâmetro é null
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)//Verifica se o valor buscado no banco retornou null
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}