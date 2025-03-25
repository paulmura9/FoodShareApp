using FoodShareNet.Domain.Entities;
using FoodShareNet.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodShareNetAPI.DTO.Beneficiary;
using FoodShareNet.Application.Interfaces;
using FoodShareNet.Application.Services;
using FoodShareNetAPI.DTO.Order;

namespace FoodShareNetAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class BeneficiaryController : ControllerBase
{
    private readonly IBeneficiaryService _beneficiaryService;
    public BeneficiaryController(IBeneficiaryService beneficiaryService)
    {
        _beneficiaryService = beneficiaryService;
    }

    [HttpGet]
    public async Task<ActionResult<IList<BeneficiaryDTO>>> GetAllBeneficiaries()
    {
       

        var beneficiaries = await _beneficiaryService.GetAllBeneficiariesAsync();

        var beneficiariesDTO = beneficiaries.Select(b => new BeneficiaryDTO
        {
            Id = b.Id,
            Name = b.Name,
            Address = b.Address,
            CityName = b.City.Name,
            Capacity = b.Capacity
        }).ToList();
        return Ok(beneficiariesDTO);
    }


    [HttpGet]
    public async Task<ActionResult<BeneficiaryDTO>> GetBeneficiary(int id)
    {
       

        var beneficiary = await _beneficiaryService.GetBeneficiaryAsync(id);

        var beneficiaryDTO = new BeneficiaryDTO
        {
            Id = beneficiary.Id,
            Name = beneficiary.Name,
            Address = beneficiary.Address,
            CityName = beneficiary.City.Name,
            Capacity = beneficiary.Capacity
        };

        return Ok(beneficiaryDTO);

    }

    [HttpPost]
    public async Task<ActionResult<BeneficiaryDetailDTO>> CreateBeneficiary(CreateBeneficiaryDTO createBeneficiaryDTO)
    {
        

        var beneficiary = new Beneficiary
        {
            Name = createBeneficiaryDTO.Name,
            Address = createBeneficiaryDTO.Address,
            CityId = createBeneficiaryDTO.CityId,
            Capacity = createBeneficiaryDTO.Capacity
        };

      

        var createBeneficiary = await _beneficiaryService.CreateBeneficiaryAsync(beneficiary);
        var beneficiaryEntityDTO = new BeneficiaryDetailDTO
        {
            Id = createBeneficiary.Id,
            Name = createBeneficiary.Name,
            Address = createBeneficiary.Address,
            CityId = createBeneficiary.CityId,
            Capacity = createBeneficiary.Capacity
        };

        return Ok(beneficiaryEntityDTO);
    }


    [HttpPut]
    public async Task<IActionResult> EditBeneficiary(int id, EditBeneficiaryDTO editBeneficiaryDTO) {
      

        if (id != editBeneficiaryDTO.Id)
        {
            return BadRequest("Mismatched Beneficiary DTO");
        }
        var editBeneficiary = new Beneficiary
        {
            Id = editBeneficiaryDTO.Id,
            Name = editBeneficiaryDTO.Name,
            Address = editBeneficiaryDTO.Address,
            CityId = editBeneficiaryDTO.CityId,
            Capacity = editBeneficiaryDTO.Capacity
        };
        await _beneficiaryService.EditBeneficiaryAsync(id, editBeneficiary);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
    {
         await _beneficiaryService.DeleteBeneficiaryAsync(id);
        return NoContent();
    }
}