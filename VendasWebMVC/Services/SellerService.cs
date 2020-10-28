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
        //Método atualizado para Assíncrono
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        //Método para cadastrar Vendedor
        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        //Método para Buscar Vendendor pelo Id
        public async Task<Seller> FindByIdAsync(int id)
        {
            //Include faz o join entre Seller e Department / Vendedor e departamento
            //Chamado de eager Loading
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        //Método para Excluír Vendedor
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException due)
            {
                throw new IntegrityException(due.Message);
            }
        }

        //Método para Atualizar Vendedor - Seller
        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if ( !hasAny )//Verificando se não Objeto Existe no Banco de Dados
            {
                throw new NotFoundException("Id não Encontrado!");
            }
            try
            {
                _context.Seller.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbe)
            {
                throw new DbConcurrencyException(dbe.Message);
            }
        }
    }
}