using BLL;
using System.Collections.Generic;
using BLL.Entities;
using DAL.Entities;
using System;
using System.Text;

namespace BLL.BookingService
{
    public class BookingService : IBookingService
    {
        IReadWriteService<Booking, BookingDAL> readWrite;

        public BookingService(IReadWriteService<Booking, BookingDAL> readWrite)
        {
            this.readWrite = readWrite; 
        }

        public void add(ulong train, ushort car, byte sit)
        {
            Booking added = new Booking(train, car, sit);
            List<Booking> bookings = readWrite.ReadData();
            bookings.Add(added);
            readWrite.WriteData(bookings);
        }

        public void delete(ulong train, ushort car, byte sit)
        {
            List<Booking> bookings = readWrite.ReadData();
            bookings.Remove(bookings.Find(x => x.trainNumber == train && x.carNumber == car));
            readWrite.WriteData(bookings);
        }

        public void bookingExists(ulong train, ushort car, byte sit, bool shouldExist)
        {
            List<Booking> bookings = readWrite.ReadData();
            Booking single = bookings.Find(x => x.trainNumber == train && x.carNumber == car && x.sitNumber == sit);
            if (single == null && shouldExist == true) throw new BookingNumberException();
        }

        public string getBooking(ulong train, ushort car, byte sit)
        {
            List<Booking> bookings = readWrite.ReadData();
            Booking single = bookings.Find(x => x.trainNumber == train && x.carNumber == car && x.sitNumber == sit);
            return single.ToString(); 
        }

        public string getBookingByDate(DateTime dateTime)
        {
            List<Booking> bookings = readWrite.ReadData();
            bookings = bookings.FindAll(x => x.reservationDate == dateTime);
            StringBuilder builder = new StringBuilder(); 
            foreach(Booking booking in bookings)
            {
                builder.Append(booking.ToString());
                builder.Append("\n");
            }
            return builder.ToString(); 
        }
    }
}
