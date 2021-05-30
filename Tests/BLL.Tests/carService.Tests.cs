using BLL;
using Moq;
using System.Collections.Generic;
using Xunit;
using BLL.Entities;
using DAL.Entities;
using BLL.CarService;
using Xunit.Abstractions;

namespace Tests.BLL.Tests
{
    public class CarService_Tests
    {
        [Fact]
        public void add_Car_Success()
        {
            List<Car> data = testData.GetCarList();

            var mock = new Mock<IReadWriteService<Car, CarDAL>>();
            mock.Setup(x => x.ReadData()).Returns(data);
            mock.Setup(x => x.WriteData(data)).Verifiable();

            CarService service = new(mock.Object);

            service.add(10000, 5555);

            mock.Verify(x => x.WriteData(data), Times.Once);
            Assert.NotNull(data.Find(x => x.trainNumber == 10000));
            Assert.NotNull(data.Find(x => x.carNumber == 5555));
        }

        [Fact]
        public void delete_Car_NoBookings()
        {
            List<Car> data = testData.GetCarList();

            var mock = new Mock<IReadWriteService<Car, CarDAL>>();
            mock.Setup(x => x.ReadData()).Returns(data);

            CarService service = new(mock.Object);

            service.delete(1039921, 12900, true);

            Assert.Null(data.Find(x => x.trainNumber == 202019));
        }
        [Fact]
        public void delete_Car_Bookings()
        {
            List<Car> data = testData.GetCarList();

            var mock = new Mock<IReadWriteService<Car, CarDAL>>();
            mock.Setup(x => x.ReadData()).Returns(data);

            CarService service = new(mock.Object);

            Assert.Throws<CarNotEmptyException>(() => service.delete(1039921, 12900, false));
        }

        [Fact]
        public void isCarEmpty_Empty()
        {
            List<Car> data = testData.GetCarList();

            var mock = new Mock<IReadWriteService<Car, CarDAL>>();
            mock.Setup(x => x.ReadData()).Returns(data);

            CarService service = new(mock.Object);

            bool actual = service.isCarEmpty(1039921, 12900);

            Assert.True(actual);
        }
        [Fact]
        public void isCarEmpty_NotEmpty()
        {
            List<Car> data = testData.GetCarList();

            var mock = new Mock<IReadWriteService<Car, CarDAL>>();
            mock.Setup(x => x.ReadData()).Returns(data);

            CarService service = new(mock.Object);

            service.sitChangeState(1039921, 12900, 1, true);
            bool actual = service.isCarEmpty(1039921, 12900);

            Assert.False(actual);
        }
        [Fact]
        public void carExists_Success()
        {
            List<Car> data = testData.GetCarList();

            var mock = new Mock<IReadWriteService<Car, CarDAL>>();
            mock.Setup(x => x.ReadData()).Returns(data);

            CarService service = new(mock.Object);

            var exception = Record.Exception(() => service.carExists(1039921, 12900, true));

            Assert.Null(exception);
        }

        [Fact]
        public void carExists_ThrowsError()
        {
            List<Car> data = testData.GetCarList();

            var mock = new Mock<IReadWriteService<Car, CarDAL>>();
            mock.Setup(x => x.ReadData()).Returns(data);

            CarService service = new(mock.Object);

            Assert.Throws<CarNumberException>(() => service.carExists(10, 12, true));
        }
        [Fact]
        public void sitChangeState_Sit_Occupied()
        {
            List<Car> data = testData.GetCarList();

            var mock = new Mock<IReadWriteService<Car, CarDAL>>();
            mock.Setup(x => x.ReadData()).Returns(data);

            CarService service = new(mock.Object);

            service.sitChangeState(1039921, 12900, 3, true);

            Assert.True(data.Find(x => x.trainNumber == 1039921 && x.carNumber == 12900).sitsTaken[3]);
        }

        [Fact]
        public void sitChangeState_Sit_Released()
        {
            List<Car> data = testData.GetCarList();

            var mock = new Mock<IReadWriteService<Car, CarDAL>>();
            mock.Setup(x => x.ReadData()).Returns(data);

            CarService service = new(mock.Object);

            service.sitChangeState(1039921, 12900, 3, true);
            service.sitChangeState(1039921, 12900, 3, false);

            Assert.False(data.Find(x => x.trainNumber == 1039921 && x.carNumber == 12900).sitsTaken[3]);
        }

        [Fact]
        public void getCarVacantSites_Success()
        {
            List<Car> data = testData.GetCarList();

            var mock = new Mock<IReadWriteService<Car, CarDAL>>();
            mock.Setup(x => x.ReadData()).Returns(data);

            CarService service = new(mock.Object);

            string expected1 = "Car number: 12900";
            string expected2 = "Sit 1 Vacant";

            string actual = service.getCarVacantSits(1039921, 12900);
            Assert.Contains(expected1, actual);
            Assert.Contains(expected2, actual);
        }

        [Fact]
        public void getTrainCars_Success()
        {
            List<Car> data = testData.GetCarListEqualTrain();

            var mock = new Mock<IReadWriteService<Car, CarDAL>>();
            mock.Setup(x => x.ReadData()).Returns(data);

            CarService service = new(mock.Object);

            string expected1 = "1200"; 
            string expected2 = "1201"; 

            string actual = service.getTrainCars(1923901);
            Assert.Contains(expected1, actual);
            Assert.Contains(expected2, actual);
        }

        [Fact]
        public void getPercentage_Success()
        {
            List<Car> data = testData.GetCarListEqualTrain();

            var mock = new Mock<IReadWriteService<Car, CarDAL>>();
            mock.Setup(x => x.ReadData()).Returns(data);

            CarService service = new(mock.Object);

            string expected1 = "1200 0%";
            string expected2 = "1201 0%";

            string actual = service.getPercentage(1923901);
            Assert.Contains(expected1, actual);
            Assert.Contains(expected2, actual);
        }
    }
}
