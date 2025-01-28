using Microsoft.AspNetCore.Mvc;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;
using Vidly.Api.Mappers;
using Vidly.Application.Repositories;

namespace Vidly.Api.Controllers;

[ApiController]
public class ReturnController(IReturnRepository returnRepository) : ControllerBase
{
	[HttpPost(ApiEndpoints.Return.Create)]
	[ProducesResponseType(typeof(RentalResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Create([FromBody]ReturnRequest request,
		CancellationToken token)
	{
		var rentalReturn = ReturnMapper.MapToReturn(request);
		
		var rental = await returnRepository.ReturnAsync(rentalReturn, token);
			
		var response = RentalMapper.MapToResponse(rental!);
		
		return Ok(response);
	}
}
