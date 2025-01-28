using System.ComponentModel.DataAnnotations;

namespace Vidly.Contracts.Requests;

public class MovieRequest
{
	
	public string? Title { get; init; }

	public int? GenreId { get; init; }
	
	public int? NumberInStock { get; init; }

	public int? DailyRentalRate { get; init; }
	
}