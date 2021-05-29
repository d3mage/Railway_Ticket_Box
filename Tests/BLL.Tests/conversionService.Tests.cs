using System.Collections.Generic;
using Xunit;
using BLL.Entities;
using DAL.Entities;
using BLL.ConversionService;
namespace Tests.BLL.Tests
{
    public class conversionService
    {
        [Fact]
        public void convert_TrainToDAL()
        {
            List<Train> trains = testData.GetTrainList();
            List<Train> expected = trains;
            List<TrainDAL> actual = new();

            ConversionService<Train, TrainDAL>.conversion(trains, actual);

            Assert.NotNull(actual);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].departure, actual[i].departure);
                Assert.Equal(expected[i].departure, actual[i].departure);
            }
        }
        [Fact]
        public void convert_DALToTrain()
        {
            List<TrainDAL> trains = testData.GetTrainDALList();
            List<TrainDAL> expected = trains;
            List<Train> actual = new();

            ConversionService<TrainDAL, Train>.conversion(trains, actual);

            Assert.NotNull(actual);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].departure, actual[i].departure);
                Assert.Equal(expected[i].departure, actual[i].departure);
            }
        }
        [Fact]
        public void convert_CarToDAL()
        {
            List<Car> cars = testData.GetCarList();
            List<Car> expected = cars;
            List<CarDAL> actual = new();

            ConversionService<Car, CarDAL>.conversion(cars, actual);

            Assert.NotNull(actual);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].trainNumber, actual[i].trainNumber);
                Assert.Equal(expected[i].carNumber, actual[i].carNumber);
                Assert.Equal(expected[i].sitsTaken, actual[i].sitsTaken);
            }
        }
        [Fact]
        public void convert_DALToCar()
        {
            List<CarDAL> trains = testData.GetCarDALList();
            List<CarDAL> expected = trains;
            List<Car> actual = new();

            ConversionService<CarDAL, Car>.conversion(trains, actual);

            Assert.NotNull(actual);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].trainNumber, actual[i].trainNumber);
                Assert.Equal(expected[i].carNumber, actual[i].carNumber);
                Assert.Equal(expected[i].sitsTaken, actual[i].sitsTaken);
            }
        }
        [Fact]
        public void convert_BookingToDAL()
        {
            List<Booking> trains = testData.GetBookingList();
            List<Booking> expected = trains;
            List<BookingDAL> actual = new();

            ConversionService<Booking, BookingDAL>.conversion(trains, actual);

            Assert.NotNull(actual);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].trainNumber, actual[i].trainNumber);
                Assert.Equal(expected[i].carNumber, actual[i].carNumber);
                Assert.Equal(expected[i].sitNumber, actual[i].sitNumber);
            }
        }
        [Fact]
        public void convert_DALToBooking()
        {
            List<BookingDAL> trains = testData.GetBookingDALList();
            List<BookingDAL> expected = trains;
            List<Booking> actual = new();

            ConversionService<BookingDAL, Booking>.conversion(trains, actual);

            Assert.NotNull(actual);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].trainNumber, actual[i].trainNumber);
                Assert.Equal(expected[i].carNumber, actual[i].carNumber);
                Assert.Equal(expected[i].sitNumber, actual[i].sitNumber);
            }
        }
    }
}


