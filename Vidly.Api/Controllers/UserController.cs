using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Vidly.Api.Filters;
using Vidly.Api.Mappers;
using Vidly.Application.Repositories;
using Vidly.Application.Services;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;

namespace Vidly.Api.Controllers;

[ApiController]
public class UserController(
	IUserRepository userRepository,
	IUserService userService
	) : ControllerBase
{
	
	[HttpPost(ApiEndpoints.User.Create)]
	[ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Create([FromBody]UserRequest request,
		CancellationToken token)
	{
		var user = UserMapper.MapToUser(request);
		
		user = await userService.CreateAsync(user, token);

		Response.Headers["x-auth-token"] = userService.GenerateAuthToken(user);
		
		var response = UserMapper.MapToResponse(user);
		
		return Ok(response);
		
	}
	
	[HttpGet(ApiEndpoints.User.Me)]
	[Authorize("Admin")]
	// [ServiceFilter(typeof(AuthFilter))]
	// [ServiceFilter(typeof(AdminFilter))]
	[ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
	public async Task<IActionResult> Me(CancellationToken token)
	{
		// var jwt  = HttpContext.Items["user"]!.ToString();
		//
		// var jsonObject = JObject.Parse( (jwt!.Split('.')[^1]) );
		//
		// var userId = int.Parse(jsonObject["Id"]!.ToString());
		
		var userId = int.Parse(HttpContext.User.FindFirst("Id")!.Value);
		
		var user = await userRepository.GetByIdAsync(userId, token);
		
		var response = UserMapper.MapToResponse(user!);
		
		return Ok(response);

	}
}