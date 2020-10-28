using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMVC.Data;
using VendasWebMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace VendasWebMVC.Services
{
    public class SalesRecordService
    {
        //Declarando Dependência, com readonly para que não possa ser alterada
        private readonly VendasWebMVCContext _context;

        //Toda vez que for instânciado um objeto SalesRecordService, automaticamente ele recebe uma
        //Instância do VendasWebMVCContext também
        public SalesRecordService(VendasWebMVCContext context)
        {
            _context = context;
        }

        //Método para Listar SalesRecord por Período SIMPLES
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            return await result
                        .Include(x => x.Seller)
                        .Include(x => x.Seller.Department)
                        .OrderByDescending(x => x.Date)
                        .ToListAsync();
        }

        //Método para Listar SalesRecord por Período AGRUPADA
        public async Task<List< IGrouping<Department, SalesRecord> >> FindByDateGroupAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            return await result
                        .Include(x => x.Seller)
                        .Include(x => x.Seller.Department)
                        .OrderByDescending(x => x.Date)
                        .GroupBy(x => x.Seller.Department)
                        .ToListAsync();
        }
    }
}