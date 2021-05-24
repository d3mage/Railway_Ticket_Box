using BLL;
using DAL;
using Moq;
using System.Collections.Generic;
using Xunit;
using BLL.Entities;
using DAL.Entities;

namespace Tests.BLL.Tests
{
    public class readWriteService
    {
        [Fact]
        public void ReadData_Success()  
        {
            var mock = new Mock<IDataContext<TrainDAL>>();
            mock.Setup(x => x.GetData()).Returns(testData.GetTrainDALList());
            var readWriteService = new ReadWriteService<Train, TrainDAL>(mock.Object);

            var expected = testData.GetTrainList();
            var actual = readWriteService.ReadData();

            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].departure, actual[i].departure);
                Assert.Equal(expected[i].departure, actual[i].departure);
            }
        }
        [Fact]
        public void ReadData_CatchException()  
        {
            var mock = new Mock<IDataContext<TrainDAL>>();
            mock.Setup(x => x.GetData()).Throws<EmptyListException>();
            var readWriteService = new ReadWriteService<Train, TrainDAL>(mock.Object);

            List<Train> expected = new List<Train>(); 
            var actual = readWriteService.ReadData();

            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);
        }

        //[Fact]
        //public void WriteData_Success()
        //{
        //    List<Train> data = testData.GetTrainList();
        //    List<TrainDAL> trainDALs = testData.GetTrainDALList(); 
        //    var mock = new Mock<IDataContext<TrainDAL>>();
        //    mock.Setup(x => x.SetData(trainDALs)).Verifiable();
        //    var readWriteService = new ReadWriteService<Train, TrainDAL>(mock.Object);

        //    readWriteService.WriteData(data);

        //    mock.Verify(x => x.SetData(trainDALs), Times.Once);
        //}
    }
}
