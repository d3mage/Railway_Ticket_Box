using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Serializable]
    public class Train
    {
        public ulong trainNumber { get; }
        public DateTime departure { get; }

        public Train() { }

        public Train(ulong train, DateTime departure)
        {
            trainNumber = train;
            this.departure = departure; 
        }
    }
}
