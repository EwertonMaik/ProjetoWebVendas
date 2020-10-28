using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMVC.Data;
using VendasWebMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace VendasWebMVC.Services
{
    //Classe Serviços do Departamento
    public class DepartmentService
    {
        //Declarando Dependência, com readonly para que não possa ser alterada
        private readonly VendasWebMVCContext _context;

        //Toda vez que for instânciado um objeto DepartmentService, automaticamente ele recebe uma
        //Instância do VendasWebMVCContext também
        public DepartmentService(VendasWebMVCContext context)
        {
            _context = context;
        }

        //Método para Listar todos Departamentos, ordenando pelo nome
        //Método Atualizado para Assíncrono
        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}