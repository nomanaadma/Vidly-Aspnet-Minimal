namespace Vidly.Contracts.Responses;

public class MovieRentalResponse
{
	public required string Title { get; init; }
	
	public required int DailyRentalRate { get; init; }
}