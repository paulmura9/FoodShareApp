
using System;
using FoodShareNet.Application.Interfaces;
using FoodShareNet.Application.Services;
using FoodShareNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;


//namespace FoodShareNet.UnitTests
//{
//    [TestFixture]
//    public class OrderServiceTest
//    {
//        private Mock<IFoodShareDbContext> _contextMock;


//        private IOrderService _orderService;

//        [SetUp]


//        [Test]
//        public void TestMethod1()
//        {
//        }
//    }
//}

//pentru create order 

namespace FoodShareNet.UnitTests
{
    [TestFixture]
    public class OrderServiceTest
    {
        private Mock<IFoodShareDbContext> _contextMock;
        private Mock<DbSet<Order>> _dbSetMock;
        private IOrderService _orderService;

        [SetUp]
        public void SetUp()
        {
            _contextMock = new Mock<IFoodShareDbContext>();
            _dbSetMock = new Mock<DbSet<Order>>();
            _contextMock.Setup(m => m.Orders).Returns(_dbSetMock.Object);
            _orderService = new OrderService(_contextMock.Object);
        }

        [Test]
        public async Task CreateOrderAsync_ShouldAddOrderToDbContext()
        {
            // Arrange
            var order = new Order
            {
                BeneficiaryId = 1,
                DonationId = 1,
                CourierId = 1,
                CreationDate = DateTime.UtcNow,
                OrderStatusId = 1
            };

            _dbSetMock.Setup(m => m.Add(It.IsAny<Order>())).Callback<Order>((o) => o.Id = 1);
            _contextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            var result = await _orderService.CreateOrderAsync(order);

            // Assert
            _dbSetMock.Verify(m => m.Add(It.IsAny<Order>()), Times.Once);
            _contextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            // Assert.AreEqual(1, result.Id);
            // Assert.Equals(1, result.Id);

            ClassicAssert.AreEqual(1, result.Id);

            if (result.Id != 1)
            {
                Assert.Fail($"Expected result.Id to be 1, but found {result.Id}.");
            }

        }

        [Test]
        public async Task GetOrderAsync_ExistingOrderId_ReturnsOrder()
        {
            // Arrange
            var orderId = 1;
            var order = new Order
            {
                Id = orderId,
                // Set other attributes for order
            };
            _dbSetMock.Setup(m => m.FindAsync(orderId)).ReturnsAsync(order);

            // Act
            var result = await _orderService.GetOrderAsync(orderId);

            // Assert
            //Assert.AreEqual(order, result);
            ClassicAssert.AreEqual(order, result);

        }

        [Test]
        public async Task UpdateOrderStatusAsync_ValidInput_ReturnsTrue()
        {
            // Arrange
            var orderId = 1;
            var newStatus = Domain.Enums.OrderStatus.Delivered;

            var order = new Order
            {
                Id = orderId,
                // Set other attributes for order
            };
            _dbSetMock.Setup(m => m.FindAsync(orderId)).ReturnsAsync(order);

            // Act
            var result = await _orderService.UpdateOrderStatusAsync(orderId, newStatus);

            // Assert
            //Assert.IsTrue(result);
            ClassicAssert.IsTrue(result);
            //Assert.AreEqual(newStatus, order.OrderStatusId);
            ClassicAssert.AreEqual(newStatus, order.OrderStatusId);
        }
    }
}