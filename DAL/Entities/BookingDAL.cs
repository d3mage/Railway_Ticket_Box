using System;

namespace DAL.Entities
{
    [Serializable]
    public class BookingDAL
    {
        public ulong trainNumber { get; }
        public ushort carNumber { get; }
        public byte sitNumber { get; }
        public DateTime reservationDate { get; }

        public BookingDAL() { }

        public BookingDAL(ulong train, ushort car, byte sit)
        {
            trainNumber = train;
            carNumber = car;
            sitNumber = sit;
            reservationDate = DateTime.Now.Date; 
        }
        public BookingDAL(ulong train, ushort car, byte sit, DateTime date)
        {
            trainNumber = train;
            carNumber = car;
            sitNumber = sit;
            reservationDate = date; 
        }
    }
}
