using System;

namespace BLL
{
    public class InvalidInputException : Exception
    {
        public override string Message => "Wrong input data provided.";
    }
}
