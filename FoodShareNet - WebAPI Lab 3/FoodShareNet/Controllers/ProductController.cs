using Microsoft.AspNetCore.Mvc;
using FoodShareNetAPI.DTO.Product;
using FoodShareNetAPI.DTO.Beneficiary;
using Microsoft.EntityFrameworkCore;
using FoodShareNet.Repository.Data;
using FoodShareNet.Application.Interfaces;


[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<IList<ProductDTO>>> GetAllProducts()
    {
        var products=await _productService.GetAllProductsAsync();
        var productsDTO = products.Select(b => new ProductDTO
        {
            Id = b.Id,
            Name = b.Name,
            Image = b.Image,
        }).ToList();
 

        return Ok(productsDTO);
    }

}
