namespace Vidly.Contracts.Responses;

public class CustomersResponse
{
	public required IEnumerable<CustomerResponse> Items { get; init; }
}

public class CustomerResponse
{
	public required int Id { get; init; }
	
	public required string Name { get; init; }
	
	public required string Phone { get; init; }
	
	public required bool IsGold { get; init; }
}