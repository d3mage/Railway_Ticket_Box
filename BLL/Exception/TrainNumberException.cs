using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TrainNumberException : Exception
    {
        public override string Message => "Entered train number is invalid.";
    }
}
