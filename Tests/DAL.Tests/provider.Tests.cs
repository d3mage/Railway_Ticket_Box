using DAL.Provider;
using DAL.Entities;
using System;
using System.Collections.Generic;
using Xunit;
using Tests.BLL.Tests;

namespace Tests.DAL.Tests
{
    public class provider_Tests
    {
        XmlProvider<int> provider = new XmlProvider<int>();
        XmlProvider<TrainDAL> providerTrain = new XmlProvider<TrainDAL>();
        XmlProvider<CarDAL> providerCar = new XmlProvider<CarDAL>();
        string conn = testData.GetIntConnectionString();
        string connTrain = testData.GetTrainConnectionString(); 
        string connCar= testData.GetCarConnectionString(); 

        [Fact]
        public void XmlProvider_Write_Read_Successfully()
        {
            List<int> expected = testData.GetIntList();

            provider.Write(expected, conn);

            var actual = provider.Read(conn);

            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }
        [Fact]
        public void XmlProvider_Write_Read_Train_Successfully()
        {
            List<TrainDAL> expected = testData.GetTrainDALList();

            providerTrain.Write(expected, connTrain);

            var actual = providerTrain.Read(connTrain);

            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].trainNumber, actual[i].trainNumber);
                Assert.Equal(expected[i].departure, actual[i].departure);
            }
        }

        [Fact]
        public void XmlProvider_Write_Read_Car_Successfully()
        {
            List<CarDAL> expected = testData.GetCarDALList();

            providerCar.Write(expected, connCar);

            var actual = providerCar.Read(connCar);

            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].trainNumber, actual[i].trainNumber);
                Assert.Equal(expected[i].carNumber, actual[i].carNumber);
            }
        }

        [Fact]
        public void XmlProvider_CatchException_Read()
        {
            string corruptedConnection = testData.GetInvalidConnectionString();
            
            List<TrainDAL> data = testData.GetTrainDALList();

            Assert.Throws<InvalidOperationException>(() => provider.Read(corruptedConnection));
        }
    }
}
