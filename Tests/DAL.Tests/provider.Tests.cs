using DAL;
using DAL.Provider;
using DAL.Entities;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests.DAL.Tests
{
    public class provider_Tests
    {
        XmlProvider<int> provider = new XmlProvider<int>();
        string conn = testData.GetConnectionString();

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
        public void XmlProvider_CatchException_Read()
        {
            string corruptedConnection = testData.GetInvalidConnectionString();
            
            List<Train> data = testData.GetTrainList();

            Assert.Throws<InvalidOperationException>(() => provider.Read(corruptedConnection));
        }
    }
}
