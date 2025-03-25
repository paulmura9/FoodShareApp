using Azure.Core;
using FoodShareNet.Application.Exceptions;
using FoodShareNet.Application.Interfaces;
using FoodShareNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Request = Azure.Core.Request;

namespace FoodShareNet.Application.Services
{
    public class OrderService:IOrderService
    {

        private readonly IFoodShareDbContext _context;

        public OrderService(IFoodShareDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            var donation = await _context.Donations
                .FirstOrDefaultAsync(d => d.Id == order.DonationId);

            // Check if the donation exists
            if (donation == null)
            {
                throw new NotFoundException("Donation", order.DonationId);
            }

            // Check if the requested quantity is available
            if (donation.Quantity < order.Quantity)
            {
                throw new OrderException($"Requested quantity exceeds available quantity for Donation ID {order.DonationId}.");
            }

            donation.Quantity -= order.Quantity;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            //
            if (order.Courier.Name == "DHL")
            {
                //HttpClient httpClient = new HttpClient();
                //var baseAdress = new Url("http://localhost:7121/api/TransportNou");
                //// HttpContent newContent
                //await httpClient.PostAsync(baseAdress, null);

                HttpClient httpClient = new HttpClient();
                var baseAddress = new Uri("http://localhost:7077/api/TransportNou");
                //var newContent = new StringContent(order.Courier.Name, System.Text.Encoding.UTF8, "application/json");
                //Request.Content = new StringContent("{\"name\":\"John Doe\",\"age\":33}",
                //                    Encoding.UTF8,
                //                    "application/json");
                //await httpClient.PostAsync(baseAddress, new StringContent(JsonConvert.SerializeObject(new { OrderId = order.Id }), Encoding.UTF8, "application/json"));

                //Req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/");
                await httpClient.PostAsync(baseAddress, null);

            }

            return order;
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            var order = await _context.Orders
            .Include(o => o.Beneficiary)
            .Include(o => o.Donation)
            .Include(o => o.OrderStatus)
            .Include(o => o.Donation.Product)
            .Include(o => o.Courier)
            .Where(o => o.Id == id)
            .FirstOrDefaultAsync();

            if (order == null)
            {
                throw new OrderException("Order does not exist");
            }

            return order;
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId,Domain.Enums.OrderStatus orderStatus)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new NotFoundException("Order",orderId);
            }


            order.OrderStatusId =(int)orderStatus;

            order.DeliveryDate = orderStatus == Domain.Enums.OrderStatus.Delivered ? DateTime.UtcNow : order.DeliveryDate;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
