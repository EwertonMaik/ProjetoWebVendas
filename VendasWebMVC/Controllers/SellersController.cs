using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VendasWebMVC.Models;
using VendasWebMVC.Models.ViewModels;
using VendasWebMVC.Services;
using VendasWebMVC.Services.Exception;

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
                return RedirectToAction(nameof(Error), new { message = "Id não Fornecido!" } );
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)//Verifica se o valor buscado no banco retornou null
            {
                return RedirectToAction(nameof(Error), new { message = "Id não Encontrado!" });
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

        //Método para exibir detalhes do cadastro do Vendedor
        public IActionResult Details(int? id)
        {
            if (id == null) //Verifica se id recebido no parâmetro é null
            {
                return RedirectToAction(nameof(Error), new { message = "Id não Fornecido!" });
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)//Verifica se o valor buscado no banco retornou null
            {
                return RedirectToAction(nameof(Error), new { message = "Id não Encontrado!" });
            }
            return View(obj);
        }

        //Método para Atualizar Cadastro Vendedor
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não Fornecido!" }); ;
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não Encontrado!" });
            }
            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não Corresponde!" });
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException ae)
            {
                return RedirectToAction(nameof(Error), new { message = ae.Message } );
            }
        }

        //Método para retornar Erro
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}