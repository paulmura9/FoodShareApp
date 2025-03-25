using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodShareNet.Domain.Entities;
using FoodShareNet.Repository.Data;
using FoodShareNetAPI.DTO.Donation;
using FoodShareNetAPI.DTO.Beneficiary;
using FoodShareNetAPI.DTO.Donor;
using FoodShareNet.Application.Interfaces;
using FoodShareNet.Application.Services;

namespace FoodShareNetAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DonationController : ControllerBase
{
    private readonly IDonationService _donationService;
    public DonationController(IDonationService donationService)
    {
        _donationService = donationService;
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<IActionResult> CreateDonation([FromBody] CreateDonationDTO donationDTO)
    {
       

        var donation = new Donation
        {
            DonorId = donationDTO.DonorId,
            ProductId = donationDTO.ProductId,
            Quantity = donationDTO.Quantity,
            ExpirationDate = donationDTO.ExpirationDate,
            StatusId = donationDTO.StatusId,
        };

        

        var createDonation = await _donationService.CreateDonationAsync(donation);
        var donationEntityDTO = new DonationDetailDTO
        {
            Id = createDonation.Id,
            DonorId = createDonation.DonorId,
            Quantity = createDonation.Quantity,
            ExpirationDate = createDonation.ExpirationDate,
            StatusId = createDonation.StatusId,
        };

        return Ok(donationEntityDTO);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<DonationDetailDTO>> GetDonation(int id)
    {
        var donation = await _donationService.GetDonationAsync(id);

        

        var donationDTO = new DonationDTO
        {
            Id = donation.Id,
            Quantity = donation.Quantity,
            ExpirationDate = donation.ExpirationDate,

            Product = donation.Product.Name,
            Donor = donation.Donor.Name,
            Status = donation.Status.Name,
        };

        if (donationDTO == null)
        {
            return NotFound();
        }

        return Ok(donationDTO);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet()]
    public async Task<ActionResult<IList<DonationDetailDTO>>> GetDonationsByCityId(int cityId)
    {
        

        var donations = await _donationService.GetDonationsByCityIdAsync(cityId);
        var donationsDTO =  donations.Select(b=>new DonationDTO
        {
            Id = b.Id,
            Quantity = b.Quantity,
            ExpirationDate = b.ExpirationDate,

            Product=b.Product.Name,
            Donor= b.Donor.Name,
            Status=b.Status.Name,
        }).ToList();

        if (donationsDTO == null)
        {
            return NotFound();
        }

        return Ok(donationsDTO);
    }

}
