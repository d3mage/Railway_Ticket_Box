﻿using BLL;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using BLL.Entities;
using DAL.Entities;
using BLL.TrainService;

namespace Tests.BLL.Tests
{
    public class TrainService_Tests
    {
        [Fact]
        public void add_Train_Success()
        {
            List<Train> data = testData.GetTrainList();

            var mock = new Mock<IReadWriteService<Train, TrainDAL>>();
            mock.Setup(x => x.ReadData()).Returns(data);
            mock.Setup(x => x.WriteData(data)).Verifiable();

            TrainService service = new(mock.Object);

            service.add(1000,"Test", "Test1", DateTime.Today);

            mock.Verify(x => x.WriteData(data), Times.Once);
            Assert.NotNull(data.Find(x => x.trainNumber == 1000));
            Assert.NotNull(data.Find(x => x.dispatch.Equals("Test")));
            Assert.NotNull(data.Find(x => x.destination.Equals("Test1")));
            Assert.NotNull(data.Find(x => x.departure == DateTime.Today));
        }

        [Fact]
        public void delete_Train_Success()
        {
            List<Train> data = testData.GetTrainList();

            var mock = new Mock<IReadWriteService<Train, TrainDAL>>();
            mock.Setup(x => x.ReadData()).Returns(data);

            TrainService service = new(mock.Object);

            service.delete(202019);

            Assert.Null(data.Find(x => x.trainNumber == 202019));
        }
    }
}
