using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Vidly.Api.Mappers;
using Vidly.Application.Repositories;
using Vidly.Application.Services;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;

namespace Vidly.Api.FastEndpoints;

public abstract class UserEndpoints
{
	private static string BaseRoute => "users";
	
	public class CreateUser(IUserService userService) 
		: Endpoint<UserRequest, UserResponse>
	{
		public override void Configure()
		{
			Post($"/{BaseRoute}");
			AllowAnonymous();

			Description(b => b
				.ProducesValidationProblem()
			);
		}
	
		public override async Task HandleAsync(
			UserRequest request,
			CancellationToken token)
		{
			
			var user = UserMapper.MapToUser(request);
	
			user = await userService.CreateAsync(user, token);
			
			var response = UserMapper.MapToResponse(user);
			
			HttpContext.Response.Headers["x-auth-token"] = userService.GenerateAuthToken(user);

			await SendOkAsync(response, token);

		}
		
	}

	public class Me(IUserRepository userRepository) 
		: EndpointWithoutRequest<UserResponse>
	{
		public override void Configure()
		{
			Get("/Me");
			Policies("Admin");
		}
	
		public override async Task HandleAsync(CancellationToken token)
		{
			
			var userId = int.Parse(HttpContext.User.FindFirst("Id")!.Value);
			
			var user = await userRepository.GetByIdAsync(userId, token);
			
			var response = UserMapper.MapToResponse(user!);
			
			await SendOkAsync(response, token);
	
		}
	}
	
}