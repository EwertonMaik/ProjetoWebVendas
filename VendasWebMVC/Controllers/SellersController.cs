﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync(); //Retorna um Lista de Vendedores
            return View(list);
        }

        //Ação para criar um vendedor
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if( ! ModelState.IsValid) //Se a requisição recebida não for válida
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        //Método para Excluir Vendedor
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) //Verifica se id recebido no parâmetro é null
            {
                return RedirectToAction(nameof(Error), new { message = "Id não Fornecido!" } );
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)//Verifica se o valor buscado no banco retornou null
            {
                return RedirectToAction(nameof(Error), new { message = "Id não Encontrado!" });
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch(IntegrityException ie)
            {
                return RedirectToAction(nameof(Error), new { message = ie.Message } );
            }
        }

        //Método para exibir detalhes do cadastro do Vendedor
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) //Verifica se id recebido no parâmetro é null
            {
                return RedirectToAction(nameof(Error), new { message = "Id não Fornecido!" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)//Verifica se o valor buscado no banco retornou null
            {
                return RedirectToAction(nameof(Error), new { message = "Id não Encontrado!" });
            }
            return View(obj);
        }

        //Método para Atualizar Cadastro Vendedor
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não Fornecido!" }); ;
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não Encontrado!" });
            }
            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid) //Se a requisição recebida não for válida
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não Corresponde!" });
            }
            try
            {
                await _sellerService.UpdateAsync(seller);
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