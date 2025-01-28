using Microsoft.AspNetCore.Mvc;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;
using Vidly.Api.Mappers;
using Vidly.Application.Repositories;

namespace Vidly.Api.Controllers;

[ApiController]
public class CustomerController(ICustomerRepository customerRepository) : ControllerBase
{
	[HttpPost(ApiEndpoints.Customer.Create)]
	[ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Create([FromBody]CustomerRequest request,
		CancellationToken token)
	{
		var customer = CustomerMapper.MapToCustomer(request);
		
		customer = await customerRepository.CreateAsync(customer, token);
		
		var response = CustomerMapper.MapToResponse(customer);
	
		return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
		
	}
	
	[HttpGet(ApiEndpoints.Customer.Get)]
	[ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Get([FromRoute] int id,
		CancellationToken token)
	{
		var customer = await customerRepository.GetByIdAsync(id, token);
		
		if (customer is null)
			return NotFound();

		var response = CustomerMapper.MapToResponse(customer);
		return Ok(response);
	}
	
	[HttpGet(ApiEndpoints.Customer.GetAll)]
	[ProducesResponseType(typeof(CustomersResponse), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetAll(CancellationToken token)
	{
		var customers = await customerRepository.GetAllAsync(token);
		var response = CustomerMapper.MapToListResponse(customers);
		return Ok(response);
	}
	
	
	[HttpPut(ApiEndpoints.Customer.Update)]
	[ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Update([FromRoute] int id, 
		[FromBody] CustomerRequest request, 
		CancellationToken token)
	{
		var existingCustomer = await customerRepository.GetByIdAsync(id, token);
		
		if (existingCustomer is null)
			return NotFound();
		
		var customer = CustomerMapper.MapToCustomerWithId(request, id);
		
		customer = await customerRepository.UpdateAsync(customer, token);
		
		var response = CustomerMapper.MapToResponse(customer);
		
		return Ok(response);
		
	}
	
	
	[HttpDelete(ApiEndpoints.Customer.Delete)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken token)
	{
		var existingCustomer = await customerRepository.GetByIdAsync(id, token);
		
		if (existingCustomer is null)
			return NotFound();
		
		await customerRepository.DeleteByIdAsync(existingCustomer, token);
		
		return Ok();
	}
	
}
