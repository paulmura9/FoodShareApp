
using NUnit.Framework;
using FoodShareNet.Application;


using System;
using FoodShareNet.Application.Interfaces;
using Moq;

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
