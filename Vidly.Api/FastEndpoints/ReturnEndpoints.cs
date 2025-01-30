using FastEndpoints;
using Vidly.Api.Mappers;
using Vidly.Application.Repositories;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;

namespace Vidly.Api.FastEndpoints;

public abstract class ReturnEndpoints
{
	private static string BaseRoute => "returns";
	
	public class CreateReturn(IReturnRepository returnRepository) 
		: Endpoint<ReturnRequest, RentalResponse>
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
			ReturnRequest request,
			CancellationToken token)
		{
			
			var rentalReturn = ReturnMapper.MapToReturn(request);

			var rental = await returnRepository.ReturnAsync(rentalReturn, token);

			var response = RentalMapper.MapToResponse(rental!);
			
			await SendOkAsync(response, token);
			
		}
		
	}

	
}