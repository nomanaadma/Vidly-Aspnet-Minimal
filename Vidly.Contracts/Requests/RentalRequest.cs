using System.ComponentModel.DataAnnotations;

namespace Vidly.Contracts.Requests;

public class RentalRequest
{
	[Required]
	public int? CustomerId { get; init; }

	[Required]
	public int? MovieId { get; init; }
}