using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodShareNet.Domain.Entities;
using FoodShareNet.Repository.Data;
using FoodShareNetAPI.DTO.Beneficiary;
using OrderStatusEnum = FoodShareNet.Domain.Enums.OrderStatus;
using FoodShareNet.Application.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class CourierController : ControllerBase
{
    private readonly ICourierService _courierService;
    public CourierController(ICourierService courierService)
    {
        _courierService = courierService;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<IList<CourierDTO>>> GetAllCourierAsync()
    {
        var couriers = await _courierService.GetAllCouriersAsync();

        var couriersDTO =  couriers.Select(b => new CourierDTO
          {
              Id = b.Id,
              Name = b.Name,
              Price = b.Price,
          }).ToList();

        return Ok(couriersDTO);
    }

}
