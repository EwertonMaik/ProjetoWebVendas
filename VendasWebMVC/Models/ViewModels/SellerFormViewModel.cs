using System.Collections.Generic;

namespace VendasWebMVC.Models.ViewModels
{
    //Classe Formulário Visualização Vendedor
    public class SellerFormViewModel
    {
        //Atributos AutoProperties
        public Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}