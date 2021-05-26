using System;

namespace BLL
{ 
    public class CarNumberException : Exception
    {
        public override string Message => "Entered car number is invalid.";
    }
}
