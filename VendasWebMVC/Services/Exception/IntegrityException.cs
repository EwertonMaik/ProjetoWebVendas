using System;

namespace VendasWebMVC.Services.Exception
{
    //Classe IntegrityException Herdando do 
    public class IntegrityException : ApplicationException
    {
        //Construtor passando parametros para sua Super Classe
        public IntegrityException(string message) : base(message)
        {
        }
    }
}