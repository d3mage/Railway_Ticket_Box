using DAL;
using DAL.Provider;
using Moq;
using System;
using Tests.BLL.Tests;
using Xunit;

namespace Tests.DAL.Tests
{
    public class context_Tests
    {
        string conn = testData.GetIntConnectionString();

        [Fact]
        public void GetData_AddSuccessfully()
        {
            var mock = new Mock<IProvider<int>>();
            mock.Setup(x => x.Read(conn)).Returns(testData.GetIntList());

            var context = new DataContext<int>(mock.Object, conn);

            var expected = testData.GetIntList();
            var actual = context.GetData();

            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }

        [Fact]
        public void GetData_ProviderNull()
        {
            IProvider<int> provider = null;

            var context = new DataContext<int>(provider, conn);

            Assert.Throws<ProviderException>(() => context.GetData());
        }

        [Fact]
        public void GetData_ReadingException()
        {
            var mock = new Mock<IProvider<int>>();
            mock.Setup(x => x.Read(conn)).Throws<Exception>();

            var context = new DataContext<int>(mock.Object, conn);

            Assert.Throws<EmptyListException>(() => context.GetData());
        }

        [Fact]
        public void SetData_AddSuccessfully()
        {
            IProvider<int> provider = new XmlProvider<int>();
            DataContext<int> context = new DataContext<int>(provider, conn);

            var expected = testData.GetIntList();
            context.SetData(testData.GetIntList());
            var actual = context.GetData();

            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }

        [Fact]
        public void SetData_ProviderNull()
        {
            IProvider<int> provider = null;

            var context = new DataContext<int>(provider, conn);

            Assert.Throws<ProviderException>(() => context.SetData(testData.GetIntList()));
        }
    }
}
