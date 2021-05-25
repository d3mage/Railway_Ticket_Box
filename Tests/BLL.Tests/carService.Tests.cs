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
            //_testOutputHelper.WriteLine(actual);
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
            System.Console.WriteLine(actual);
            Assert.Contains(expected1, actual);
            Assert.Contains(expected2, actual);
        }

        private readonly ITestOutputHelper _testOutputHelper;
        public CarService_Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
    }
}
