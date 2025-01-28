using Microsoft.AspNetCore.Mvc;
using Vidly.Api.Mappers;
using Vidly.Application.Repositories;
using Vidly.Application.Services;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;

namespace Vidly.Api.Endpoints;

public class UserEndpoints : IEndpoints
{
	private const string BaseRoute = "users";
	private const string Tag = "User";

	public static void DefineEndpoints(IEndpointRouteBuilder app)
	{
		var group = app.BaseGroup();

		group.MapPost($"{BaseRoute}/", Create)
			.Produces<UserResponse>(201)
			.Produces<ValidationProblemDetails>(400)
			.WithMethodName(Tag);

		group.MapGet("/me", Me)
			.Produces<UserResponse>(200)
			.RequireAuthorization("Admin")
			.WithMethodName(Tag);
		
	}

	private static async Task<IResult> Create(
		[FromBody] UserRequest request,
		HttpContext context,
		IUserService userService,
		CancellationToken token)
	{
		var user = UserMapper.MapToUser(request);

		user = await userService.CreateAsync(user, token);
		
		context.Response.Headers["x-auth-token"] = userService.GenerateAuthToken(user);

		var response = UserMapper.MapToResponse(user);
		
		return Results.Ok(response);
		
	}

	private static async Task<IResult> Me(
		HttpContext context,
		IUserRepository userRepository,
		CancellationToken token)
	{
		
		var userId = int.Parse(context.User.FindFirst("Id")!.Value);
		
		var user = await userRepository.GetByIdAsync(userId, token);
		
		var response = UserMapper.MapToResponse(user!);
		
		return Results.Ok(response);
		
	}
	
}