using System;

namespace BLL
{
    public class TrainNumberException : Exception
    {
        public override string Message => "Entered train number is invalid.";
    }
}
