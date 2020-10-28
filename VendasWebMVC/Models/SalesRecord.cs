using System;
using System.ComponentModel.DataAnnotations;
using VendasWebMVC.Models.Enums;

namespace VendasWebMVC.Models
{
    //Classe Entidade Registro de Vendas
    public class SalesRecord
    {
        //Atributos AutoProperties
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyyy}")]
        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Amount { get; set; }
        public SaleStatus Status { get; set; }
        public Seller Seller { get; set; }

        //Construtores
        public SalesRecord() { }

        public SalesRecord(int id, DateTime date, double amount, SaleStatus status, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }
    }
}