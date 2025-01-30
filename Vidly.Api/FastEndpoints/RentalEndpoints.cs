using FastEndpoints;
using Vidly.Api.Mappers;
using Vidly.Application.Repositories;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;

namespace Vidly.Api.FastEndpoints;

public abstract class RentalEndpoints
{
	private static string BaseRoute => "rentals";
	
	public class CreateRental(IRentalRepository rentalRepository) 
		: Endpoint<RentalRequest, RentalResponse>
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
			RentalRequest request,
			CancellationToken token)
		{
			
			var rental = RentalMapper.MapToRental(request);
	
			rental = await rentalRepository.CreateAsync(rental, token);
	
			var response = RentalMapper.MapToResponse(rental);
			
			await SendCreatedAtAsync<GetRental>(
				new { id = response.Id },
				response,
				generateAbsoluteUrl: true,
				cancellation: token);
			
		}
		
	}

	
	public class GetRental(IRentalRepository rentalRepository) 
		: EndpointWithoutRequest<RentalResponse>
	{
		public override void Configure()
		{
			Get($"{BaseRoute}/{{id:int}}");
			AllowAnonymous();
			
			Description(b => b
				.Produces(404)
			);
		}
	
		public override async Task HandleAsync(CancellationToken token)
		{

			var id = Route<int>("id");
			
			var rental = await rentalRepository.GetByIdAsync(id, token);
	
			if (rental is null)
			{
				await SendNotFoundAsync(token);
				return;
			}
			
			var response = RentalMapper.MapToResponse(rental);
			
			await SendOkAsync(response, token);
	
		}
	}
	
	
	public class GetAllRental(IRentalRepository rentalRepository) 
		: EndpointWithoutRequest<RentalsResponse>
	{
		public override void Configure()
		{
			Get($"{BaseRoute}");
			AllowAnonymous();
			
		}
	
		public override async Task HandleAsync(CancellationToken token)
		{
			
			var rentals = await rentalRepository.GetAllAsync(token);
			var response = RentalMapper.MapToListResponse(rentals);
			
			await SendOkAsync(response, token);
	
		}
	}
	
}