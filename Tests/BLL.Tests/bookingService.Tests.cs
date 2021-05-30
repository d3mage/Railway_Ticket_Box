using BLL;
using Moq;
using System.Collections.Generic;
using Xunit;
using BLL.Entities;
using DAL.Entities;
using BLL.BookingService;
using System;

namespace Tests.BLL.Tests
{
    public class BookingService_Tests
    {
        [Fact]
        public void add_Booking_Success()
        {
            List<Booking> data = testData.GetBookingList();

            var mock = new Mock<IReadWriteService<Booking, BookingDAL>>();
            mock.Setup(x => x.ReadData()).Returns(data);
            mock.Setup(x => x.WriteData(data)).Verifiable();

            BookingService service = new(mock.Object);

            service.add(2342934, 1030, 3);

            mock.Verify(x => x.WriteData(data), Times.Once);
            Assert.NotNull(data.Find(x => x.trainNumber == 2342934));
            Assert.NotNull(data.Find(x => x.carNumber == 1030));
            Assert.NotNull(data.Find(x => x.sitNumber == 3));
        }

        [Fact]
        public void delete_Bookings_Success()
        {
            List<Booking> data = testData.GetBookingList();

            var mock = new Mock<IReadWriteService<Booking, BookingDAL>>();
            mock.Setup(x => x.ReadData()).Returns(data);

            BookingService service = new(mock.Object);

            service.delete(2342934, 1030, 2);

            Assert.Null(data.Find(x => x.trainNumber == 202019));
        }

        [Fact]
        public void bookingExists_Success()
        {
            List<Booking> data = testData.GetBookingList();

            var mock = new Mock<IReadWriteService<Booking, BookingDAL>>();
            mock.Setup(x => x.ReadData()).Returns(data);

            BookingService service = new(mock.Object);

            var exception = Record.Exception(() => service.bookingExists(2342934, 1030, 1, true));

            Assert.Null(exception);
        }

        [Fact]
        public void carExists_ThrowsError()
        {
            List<Booking> data = testData.GetBookingList();

            var mock = new Mock<IReadWriteService<Booking, BookingDAL>>();
            mock.Setup(x => x.ReadData()).Returns(data);

            BookingService service = new(mock.Object);

            Assert.Throws<BookingNumberException>(() => service.bookingExists(10, 12, 5, true));
        }

        [Fact]
        public void getCarVacantSites_Success()
        {
            List<Booking> data = testData.GetBookingList();

            var mock = new Mock<IReadWriteService<Booking, BookingDAL>>();
            mock.Setup(x => x.ReadData()).Returns(data);

            BookingService service = new(mock.Object);

            string expected1 = "2342934 1030 1 " + DateTime.Now.Date.ToString();

            string actual = service.getBooking(2342934, 1030, 1);
            Assert.Contains(expected1, actual);
        }

        [Fact]
        public void getTrainCars_Success()
        {
            List<Booking> data = testData.GetBookingList();

            var mock = new Mock<IReadWriteService<Booking, BookingDAL>>();
            mock.Setup(x => x.ReadData()).Returns(data);

            BookingService service = new(mock.Object);

            string expected1 = "2342934 1030 1 " + DateTime.Now.Date.ToString();
            string expected2 = "2342934 1030 2 " + DateTime.Now.Date.ToString();

            string actual = service.getBookingByDate(DateTime.Now.Date);
            Assert.Contains(expected1, actual);
            Assert.Contains(expected2, actual);
        }
    }
}
