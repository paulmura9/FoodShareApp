using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using FoodShareNet.Application
using System;

namespace FoodShareNet.UnitTests
{
    [TestFixture]
    public class OrderServiceTest
    {
        private Mock<IFoodShareDbContext> _contextMock;


        private IOrderService _orderService;

        [SetUp]


        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
