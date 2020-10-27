using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMVC.Data;
using VendasWebMVC.Models;

namespace VendasWebMVC.Services
{
    //Classe Serviços de Vendedor
    public class SellerService
    {
        //Declarando Dependência, com readonly para que não possa ser alterada
        private readonly VendasWebMVCContext _context;

        //Toda vez que for instânciado um objeto SellerService, automaticamente ele recebe uma
        //Instância do VendasWebMVCContext também
        public SellerService(VendasWebMVCContext context)
        {
            _context = context;
        }

        //Método para Listar todos Vendedores
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        //Método para cadastrar Vendedor
        public void Insert(Seller obj)
        {
            obj.Department = _context.Department.First();
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}