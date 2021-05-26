using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BookingNumberException : Exception
    {
        public override string Message => "Entered booking number is invalid.";
    }
}
