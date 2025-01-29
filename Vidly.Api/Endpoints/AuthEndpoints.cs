using Microsoft.AspNetCore.Mvc;
using Vidly.Api.Endpoints.Internal;
using Vidly.Api.Mappers;
using Vidly.Application.Repositories;
using Vidly.Application.Services;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;

namespace Vidly.Api.Endpoints;

public class AuthEndpoints : IEndpoints
{
	private const string BaseRoute = "auth";
	private const string Tag = "Auth";

	public static void DefineEndpoints(IEndpointRouteBuilder app)
	{
		var group = app.BaseGroup().MapGroup(BaseRoute);

		group.MapPost("/", Auth)
			.Produces(200)
			.Produces<ValidationProblemDetails>(400)
			.WithMethodName(Tag);
		
	}

	private static async Task<IResult> Auth(
		[FromBody]AuthRequest request,
		IUserService userService,
		CancellationToken token)
	{
		var authToken = await userService.GetAuthAsync(request, token);
		return Results.Ok(authToken);
	}
	
}