using FastEndpoints;
using Vidly.Api.Mappers;
using Vidly.Application.Services;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;

namespace Vidly.Api.FastEndpoints;

public abstract class AuthEndpoints
{
	private static string BaseRoute => "auth";
	
	public class Auth(IUserService userService) 
		: Endpoint<AuthRequest>
	{
		public override void Configure()
		{
			Post($"{BaseRoute}");
			AllowAnonymous();

			Description(b => b
				.ProducesValidationProblem()
			);
			
		}
	
		public override async Task HandleAsync(
			AuthRequest request,
			CancellationToken token)
		{
			var authToken = await userService.GetAuthAsync(request, token);
			await SendOkAsync(authToken, token);
		}
		
	}
}