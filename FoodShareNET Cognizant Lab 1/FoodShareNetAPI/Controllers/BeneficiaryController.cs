﻿using Microsoft.AspNetCore.Mvc;
using FoodShareNetAPI.DTO.Beneficiary;

namespace FoodShareNetAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class BeneficiaryController : ControllerBase
{

    public BeneficiaryController()
    {

    }


    [ProducesResponseType(typeof(IList<BeneficiaryDTO>), 
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<IList<BeneficiaryDTO>>> GetAllAsync()
    {        
        return Ok();
    }


















    
    
    
    
    
    [ProducesResponseType(typeof(BeneficiaryDTO), 
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<BeneficiaryDTO>> GetAsync(int? id)
    {
        return Ok();
    }
















   
    
    
    
    
    [ProducesResponseType(typeof(BeneficiaryDetailDTO), 
        StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<BeneficiaryDetailDTO>> 
        CreateAsync(CreateBeneficiaryDTO createBeneficiaryDTO)
    {
       return Ok();
    }

    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut]
    public async Task<IActionResult> 
        EditAsync(int id, EditBeneficiaryDTO editBeneficiaryDTO)
    {
        return Ok();
    }

    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        return Ok();
    }
}
