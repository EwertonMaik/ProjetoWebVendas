using System;

namespace VendasWebMVC.Services.Exception
{
    public class DbConcurrencyException : ApplicationException
    {
        //Construtor passando chamada para Super Classe base
        public DbConcurrencyException(string message) : base(message)
        {
        }
    }
}