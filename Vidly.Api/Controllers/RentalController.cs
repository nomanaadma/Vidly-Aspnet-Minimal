using Microsoft.AspNetCore.Mvc;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;
using Vidly.Api.Mappers;
using Vidly.Application.Repositories;

namespace Vidly.Api.Controllers;

[ApiController]
public class RentalController(IRentalRepository rentalRepository) : ControllerBase
{
	[HttpPost(ApiEndpoints.Rental.Create)]
	[ProducesResponseType(typeof(RentalResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Create([FromBody]RentalRequest request,
		CancellationToken token)
	{
		var rental = RentalMapper.MapToRental(request);
		
		rental = await rentalRepository.CreateAsync(rental, token);
		
		var response = RentalMapper.MapToResponse(rental);
		
		return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
		
	}
	
	[HttpGet(ApiEndpoints.Rental.Get)]
	[ProducesResponseType(typeof(RentalResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Get([FromRoute] int id,
		CancellationToken token)
	{
		var rental = await rentalRepository.GetByIdAsync(id, token);
		
		if (rental is null)
			return NotFound();
	
		var response = RentalMapper.MapToResponse(rental);
		return Ok(response);
	}
	
	[HttpGet(ApiEndpoints.Rental.GetAll)]
	[ProducesResponseType(typeof(RentalsResponse), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetAll(CancellationToken token)
	{
		var rentals = await rentalRepository.GetAllAsync(token);
		var response = RentalMapper.MapToListResponse(rentals);
		return Ok(response);
	}
	
}
