using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BookingService
{
    public interface IBookingService
    {
        public void add(ulong train, ushort car, byte sit);
        public void delete(ulong train, ushort car, byte sit);
        public void bookingExists(ulong train, ushort car, byte sit, bool shouldExist);
        public string getBooking(ulong train, ushort car, byte sit);
        public string getBookingByDate(DateTime dateTime);
    }
}
