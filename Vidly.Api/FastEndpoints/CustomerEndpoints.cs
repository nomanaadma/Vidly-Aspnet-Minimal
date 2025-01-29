using FastEndpoints;
using Vidly.Api.Mappers;
using Vidly.Application.Repositories;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;

namespace Vidly.Api.FastEndpoints;

public abstract class CustomerEndpoints
{
	private static string BaseRoute => "customers";
	
	public class CreateCustomer(ICustomerRepository customerRepository) 
		: Endpoint<CustomerRequest, CustomerResponse>
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
			CustomerRequest request,
			CancellationToken token)
		{
			
			var customer = CustomerMapper.MapToCustomer(request);
	
			customer = await customerRepository.CreateAsync(customer, token);
	
			var response = CustomerMapper.MapToResponse(customer);
			
			await SendCreatedAtAsync<GetCustomer>(
				new { id = response.Id },
				response,
				generateAbsoluteUrl: true,
				cancellation: token);
			
		}
		
	}

	
	public class GetCustomer(ICustomerRepository customerRepository) 
		: EndpointWithoutRequest<CustomerResponse>
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
			
			var customer = await customerRepository.GetByIdAsync(id, token);
	
			if (customer is null)
			{
				await SendNotFoundAsync(token);
				return;
			}
			
			var response = CustomerMapper.MapToResponse(customer);
			
			await SendOkAsync(response, token);
	
		}
	}
	
	
	public class GetAllCustomer(ICustomerRepository customerRepository) 
		: EndpointWithoutRequest<CustomersResponse>
	{
		public override void Configure()
		{
			Get($"{BaseRoute}");
			AllowAnonymous();
		}
	
		public override async Task HandleAsync(CancellationToken token)
		{
			
			var customers = await customerRepository.GetAllAsync(token);
			var response = CustomerMapper.MapToListResponse(customers);
			
			await SendOkAsync(response, token);
	
		}
	}
	
	
	public class UpdateCustomer(ICustomerRepository customerRepository) 
		: Endpoint<CustomerRequest, CustomerResponse>
	{
		public override void Configure()
		{
			Put($"{BaseRoute}/{{id:int}}");
			AllowAnonymous();
			Description(b => b
				.Produces(404)
				.ProducesValidationProblem()
			);
		}
	
		public override async Task HandleAsync(
			CustomerRequest request,
			CancellationToken token)
		{
			var id = Route<int>("id");
			
			var existingCustomer = await customerRepository.GetByIdAsync(id, token);

			if (existingCustomer is null)
			{
				await SendNotFoundAsync(token);
				return;
			}

			var customer = CustomerMapper.MapToCustomerWithId(request, id);

			customer = await customerRepository.UpdateAsync(customer, token);

			var response = CustomerMapper.MapToResponse(customer);

			await SendOkAsync(response, token);
			
		}
		
	}
	
	
	public class DeleteCustomer(ICustomerRepository customerRepository) 
		: EndpointWithoutRequest
	{
		public override void Configure()
		{
			Delete($"{BaseRoute}/{{id:int}}");
			AllowAnonymous();
			Description(b => b
				.Produces(404)
				.ProducesValidationProblem()
			);
		}
	
		public override async Task HandleAsync(
			CancellationToken token)
		{
			
			var id = Route<int>("id");
			var existingCustomer = await customerRepository.GetByIdAsync(id, token);

			if (existingCustomer is null)
			{
				await SendNotFoundAsync(token);
				return;
			}

			await customerRepository.DeleteByIdAsync(existingCustomer, token);

			await SendNoContentAsync(token);

		}
		
	}
	
}