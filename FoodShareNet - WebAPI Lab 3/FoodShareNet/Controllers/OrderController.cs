using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodShareNet.Domain.Entities;
using FoodShareNet.Repository.Data;
using FoodShareNetAPI.DTO.Order;
using OrderStatusEnum = FoodShareNet.Domain.Enums.OrderStatus;
using FoodShareNet.Application.Interfaces;
using FoodShareNet.Application.Exceptions;
using FoodShareNet.Application.Services;

[Route("api/[controller]/[action]")]
[ApiController]



public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // POST: api/Order
    [HttpPost]
    public async Task<ActionResult<OrderDTO>> CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
    {
        var order = new Order
        {
            BeneficiaryId = createOrderDTO.BeneficiaryId,
            DonationId = createOrderDTO.DonationId,
            CourierId = createOrderDTO.CourierId,
            CreationDate = createOrderDTO.CreationDate,
            OrderStatusId = createOrderDTO.OrderStatusId,
        };

        try
        {
            var createOrder = await _orderService.CreateOrderAsync(order);
            var orderDetails = new OrderDetailsDTO
            {
                Id = createOrder.Id,
                BeneficiaryId = createOrder.BeneficiaryId,
                DonationId = createOrder.DonationId,
                CourierId = createOrder.CourierId,
                CreationDate = createOrder.CreationDate,
                DeliveryDate = createOrder.DeliveryDate,
                OrderStatusId = createOrder.OrderStatusId,
            };
            return Ok(orderDetails);

        }
        catch (NotFoundException)
        {
            return NotFound($"Donation with ID {createOrderDTO.DonationId} not found.");
        }
        catch (OrderException)
        {
            return BadRequest($"Requested quantity exceeds available quantity for Donation ID {createOrderDTO.DonationId}.");
        }
    }

    // GET: api/Order/5
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDTO>> GetOrder(int id)
    {
        try
        {
            var order = await _orderService.GetOrderAsync(id);
            var orderDTO = new OrderDTO
            {
                Id = order.Id,
                BeneficiaryId = order.BeneficiaryId,
                BeneficiaryName = order.Beneficiary.Name,
                DonationProduct = order.Donation.Product.Name,
                DonationId = order.DonationId,
                CourierId = order.CourierId,
                CourierName = order.Courier.Name,
                CreationDate = order.CreationDate,
                DeliveryDate = order.DeliveryDate,
                OrderStatusId = order.OrderStatusId,
                OrderStatusName = order.OrderStatus.Name,
            };
            return Ok(order);

        }
        catch (NotFoundException)
        {
            return NotFound("Order does not exist");
        }
    }

    [HttpPatch("{orderId:int}/status")]
    public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] UpdateOrderStatusDTO updateStatusDTO)
    {
        try
        {
            if (orderId!=updateStatusDTO.OrderId) {
                return BadRequest("Mismatched Order ID");
            }

            await _orderService.UpdateOrderStatusAsync(orderId, (OrderStatusEnum)updateStatusDTO.NewStatusId);
        }catch (NotFoundException) {
            return NotFound();
        } 
        return NoContent();
    }
}