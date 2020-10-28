using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace VendasWebMVC.Models
{
    //Classe Entidade Vendedor
    public class Seller
    {
        //Atributos AutoProperties
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} Obrigatório!")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Tamanho do {0} deve ser Entre {2} e {1}!")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Digite um E-mail Válido!")]
        [Required(ErrorMessage = "{0} Obrigatório!")]
        public string Email { get; set; }

        [Display(Name = "Data Aniv.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "{0} Obrigatório!")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Salário Base")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Required(ErrorMessage = "{0} Obrigatório!")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} deve ser de {1} até no máximo {2}")]
        public double BaseSalary { get; set; }

        [Display(Name = "Departamento")]
        public Department Department { get; set; }

        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        //Construtores
        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        //Método Para Adicionar Vendas
        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }
        
        //Método Para Remover Vendas
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        //Métod Para Retornar o Total de Vendas Por Período
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}