using Riok.Mapperly.Abstractions;
using Vidly.Application.Models;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;

namespace Vidly.Api.Mappers;

[Mapper]
public static partial class CustomerMapper
{
	public static partial CustomerResponse MapToResponse(Customer customer);
	
	public static CustomersResponse MapToListResponse(IEnumerable<Customer> customers) => 
		new() { Items = customers.Select(MapToResponse) };

	public static partial Customer MapToCustomer(CustomerRequest customerRequest);

	public static Customer MapToCustomerWithId(CustomerRequest customerRequest, int id)
	{
		var customer = MapToCustomer(customerRequest);
		customer.Id = id;
		return customer;
	}

}