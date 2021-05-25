using System;

namespace BLL
{
    public class CarNotEmptyException : Exception
    {
        public override string Message => "Car cannot be deleted: some sits are booked.";
    }
}
