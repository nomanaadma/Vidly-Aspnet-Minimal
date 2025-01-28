using Microsoft.AspNetCore.Mvc;
using Vidly.Application.Services;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;

namespace Vidly.Api.Controllers;

[ApiController]
public class AuthController(IUserService userService) : ControllerBase
{
	[HttpPost(ApiEndpoints.Auth)]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Auth([FromBody]AuthRequest request,
		CancellationToken token)
	{
		var authToken = await userService.GetAuthAsync(request, token);
		return Ok(authToken);
	}
}