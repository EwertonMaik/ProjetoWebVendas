using System;
using System.Collections.Generic;
using System.Linq;

namespace VendasWebMVC.Models
{
    //classe Entidade Departamentos
    public class Department
    {
        //Atributos AutoProperties
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        //Construtores
        public Department() { }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        //Método Para Adicionar um Vendedor
        public void AddSaller(Seller seller)
        {
            Sellers.Add(seller);
        }

        //Método Para Calcular o Total de Vendas Por Departamento
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(seller =>  seller.TotalSales(initial, final) );
        }

    }
}