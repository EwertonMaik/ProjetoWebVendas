using System;

namespace VendasWebMVC.Services.Exception
{
    //Classe NotFoundException herda de ApplicationException
    public class NotFoundException : ApplicationException
    {
        //Construtor passando chamada para Super Classe base
        public NotFoundException(string message) : base(message)
        {

        }
    }
}