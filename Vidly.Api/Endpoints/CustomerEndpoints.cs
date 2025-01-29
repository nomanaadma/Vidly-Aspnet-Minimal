using Microsoft.AspNetCore.Mvc;
using Vidly.Api.Endpoints.Internal;
using Vidly.Api.Mappers;
using Vidly.Application.Repositories;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;

namespace Vidly.Api.Endpoints;

public class CustomerEndpoints : IEndpoints
{
	private const string BaseRoute = "customers";
	private const string Tag = "Customer";

	public static void DefineEndpoints(IEndpointRouteBuilder app)
	{
		// var group = app.BaseGroup().MapGroup(BaseRoute);
		//
		// group.MapPost("/", Create)
		// 	.Produces<CustomerResponse>(201)
		// 	.Produces<ValidationProblemDetails>(400)
		// 	.WithMethodName(Tag);
		//
		// group.MapGet("/{id:int}", Get)
		// 	.Produces<CustomerResponse>(200)
		// 	.Produces(404)
		// 	.WithMethodName(Tag);
		//
		// group.MapGet("/", GetAll)
		// 	.Produces<CustomersResponse>(200)
		// 	.WithMethodName(Tag);
		//
		// group.MapPut("/{id:int}", Update)
		// 	.Produces<CustomerResponse>(200)
		// 	.Produces<ValidationProblemDetails>(400)
		// 	.Produces(404)
		// 	.WithMethodName(Tag);
		//
		// group.MapDelete("/{id:int}", Delete)
		// 	.Produces(204)
		// 	.Produces(404)
		// 	.WithMethodName(Tag);
		
	}

	private static async Task<IResult> Create(
		[FromBody] CustomerRequest request,
		ICustomerRepository customerRepository,
		CancellationToken token)
	{
		var customer = CustomerMapper.MapToCustomer(request);

		customer = await customerRepository.CreateAsync(customer, token);

		var response = CustomerMapper.MapToResponse(customer);
		return Results.CreatedAtRoute($"Get{Tag}", new { id = response.Id }, response);
	}

	private static async Task<IResult> Get(
		[FromRoute] int id,
		ICustomerRepository customerRepository,
		CancellationToken token)
	{
		var customer = await customerRepository.GetByIdAsync(id, token);

		if (customer is null)
			return Results.NotFound();

		var response = CustomerMapper.MapToResponse(customer);
		return Results.Ok(response);
	}

	private static async Task<IResult> GetAll(
		ICustomerRepository customerRepository,
		CancellationToken token)
	{
		var customers = await customerRepository.GetAllAsync(token);
		var response = CustomerMapper.MapToListResponse(customers);
		return Results.Ok(response);
	}

	private static async Task<IResult> Update(
		[FromRoute] int id,
		[FromBody] CustomerRequest request,
		ICustomerRepository customerRepository,
		CancellationToken token)
	{
		var existingCustomer = await customerRepository.GetByIdAsync(id, token);

		if (existingCustomer is null)
			return Results.NotFound();

		var customer = CustomerMapper.MapToCustomerWithId(request, id);

		customer = await customerRepository.UpdateAsync(customer, token);

		var response = CustomerMapper.MapToResponse(customer);

		return Results.Ok(response);
	}

	private static async Task<IResult> Delete(
		[FromRoute] int id,
		ICustomerRepository customerRepository,
		CancellationToken token)
	{
		var existingCustomer = await customerRepository.GetByIdAsync(id, token);

		if (existingCustomer is null)
			return Results.NotFound();

		await customerRepository.DeleteByIdAsync(existingCustomer, token);

		return Results.NoContent();
	}
	
}