using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Serializable]
    public class Booking
    {
        public ulong trainNumber { get; }
        public ushort carNumber { get; }
        public ushort sitNumber { get; }
        public DateTime reservationDate { get; }

        public Booking() { }

        public Booking(ulong train, ushort car, ushort sit)
        {
            trainNumber = train;
            carNumber = car;
            sitNumber = sit;
            reservationDate = DateTime.Now; 
        }
    }
}
