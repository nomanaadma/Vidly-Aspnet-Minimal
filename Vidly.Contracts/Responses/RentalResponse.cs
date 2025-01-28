namespace Vidly.Contracts.Responses;

public class RentalsResponse
{
	public required IEnumerable<RentalResponse> Items { get; init; }
}

public class RentalResponse
{
	public required int Id { get; init; }
	
	public required CustomerResponse Customer { get; init; }

	public required MovieRentalResponse Movie { get; init; }

	public required DateTime DateOut { get; init; }
	
	public DateTime? DateReturned { get; init; }
	
	public required int RentalFee { get; init; }
}