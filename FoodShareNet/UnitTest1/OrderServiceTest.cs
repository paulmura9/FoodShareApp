using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq
using NUnit.Framework;
using System;

namespace FoodShareNet.UnitTests
{
    [TestFixture]
    public class OrderServiceTest
    {
        private Mock<IFoodShareDbContext> _contextMock;


        private IOrderService _orderService;

        [SetUp]

        [Test]
        public void TestMethod1()
        {

        }
    }
}
