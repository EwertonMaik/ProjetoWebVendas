using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMVC.Data;
using VendasWebMVC.Models;
using Microsoft.EntityFrameworkCore;
using VendasWebMVC.Services.Exception;

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
            _context.Add(obj);
            _context.SaveChanges();
        }

        //Método para Buscar Vendendor pelo Id
        public Seller FindById(int id)
        {
            //Include faz o join entre Seller e Department / Vendedor e departamento
            //Chamado de eager Loading
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        //Método para Excluír Vendedor
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        //Método para Atualizar Vendedor - Seller
        public void Update(Seller obj)
        {
            if ( !_context.Seller.Any(x => x.Id == obj.Id) )//Verificando se não Objeto Existe no Banco de Dados
            {
                throw new NotFoundException("Id não Encontrado!");
            }
            try
            {
                _context.Seller.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException dbe)
            {
                throw new DbConcurrencyException(dbe.Message);
            }
        }
    }
}