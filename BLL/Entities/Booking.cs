using System;

namespace BLL.Entities
{
    public class Booking
    {
        public ulong trainNumber { get; }
        public ushort carNumber { get; }
        public byte sitNumber { get; }
        public DateTime reservationDate { get; }

        public Booking(ulong train, ushort car, byte sit)
        {
            trainNumber = train;
            carNumber = car;
            sitNumber = sit;
            reservationDate = DateTime.Now; 
        }
    }
}
