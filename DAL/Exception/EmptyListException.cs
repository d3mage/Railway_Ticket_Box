using System;

namespace DAL
{
    public class EmptyListException : Exception
    {
        public override string Message => "You need to add to list first.";
    }
}
