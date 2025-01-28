using System.ComponentModel.DataAnnotations;

namespace Vidly.Contracts.Requests;

public class MovieRequest
{
	
	[Required]
	public string? Title { get; init; }

	[Required]
	public int? GenreId { get; init; }
	
	[Required]
	public int? NumberInStock { get; init; }

	[Required]
	public int? DailyRentalRate { get; init; }
	
}