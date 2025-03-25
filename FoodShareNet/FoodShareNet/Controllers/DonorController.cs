using FoodShareNet.Domain.Entities;
using FoodShareNet.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodShareNetAPI.DTO.Donor;
using FoodShareNetAPI.DTO.Donation;
using FoodShareNet.Application.Interfaces;
using System.Drawing;
namespace FoodShareNetAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DonorController : ControllerBase
{
    private readonly IDonorService _donorService;

    public DonorController(IDonorService donorService)
    {
        _donorService = donorService;
    }

    [ProducesResponseType(type: typeof(List<DonorDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<IList<DonorDTO>>> GetAllDonors()
    {
        var donors = await _donorService.GetAllDonorsAsync();
        var donorsDTO = donors.Select(b=>new DonorDTO
        {
            Id = b.Id,
            Name = b.Name,
            Address = b.Address,
            CityName = b.City.Name,

            Donations = b.Donations?.Select(donation => new DonationDTO
            {
                Id = donation.Id,
                Quantity = donation.Quantity,
                ExpirationDate = donation.ExpirationDate,

                Product = donation.Product?.Name, 
                Donor = donation.Donor?.Name, 
                Status = donation.Status?.Name,
            }).ToList() ?? new List<DonationDTO>()
        }).ToList();

    

        return Ok(donorsDTO);
    }
    [ProducesResponseType(type: typeof(List<DonorDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet()]
    public async Task<ActionResult<DonorDTO>> GetDonor(int id)
    {
        var donor = await _donorService.GetDonorAsync(id);

        var donorDTO = new DonorDTO
        {
            Id = donor.Id,
            Name = donor.Name,
            Address = donor.Address,
            CityName = donor.City.Name,

            Donations = donor.Donations?.Select(donation => new DonationDTO
            {
                Id = donation.Id,
                Quantity = donation.Quantity,
                ExpirationDate = donation.ExpirationDate,

                Product = donation.Product?.Name,
                Donor = donation.Donor?.Name,
                Status = donation.Status?.Name,
            }).ToList() ?? new List<DonationDTO>()
        };
    

        if (donorDTO == null)
        {
            return NotFound();
        }

        return Ok(donorDTO);
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<DonorDetailDTO>> CreateDonor(CreateDonorDTO createDonorDTO)
    {
     

        var donor = new Donor
        {
            Name = createDonorDTO.Name,
            CityId = createDonorDTO.CityId,
            Address = createDonorDTO.Address
        };

        

        var createDonor=await _donorService.CreateDonorAsync(donor);
        var donorEntityDTO = new DonorDetailDTO
        {
            Id = createDonor.Id,
            Name = createDonor.Name,
            CityId = createDonor.CityId,
            Address = createDonor.Address
        };

        return Ok(donorEntityDTO);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut()]
    public async Task<IActionResult> EditAsync(int id, EditDonorDTO editDonorDTO)
    {
        if (id != editDonorDTO.Id)
        {
            return BadRequest("Mismatched Donor ID");
        }

        

        var editDonor = new Donor
        {
            Id = editDonorDTO.Id,
            Name = editDonorDTO.Name,
            CityId = editDonorDTO.CityId,
            Address = editDonorDTO.Address,
        };


        await _donorService.EditDonorAsync(id, editDonor);

        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete()]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        

        await _donorService.DeleteDonorAsync(id);
        return NoContent();
    }
}
