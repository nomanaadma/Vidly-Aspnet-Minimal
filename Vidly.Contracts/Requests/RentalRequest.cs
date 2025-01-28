using System.ComponentModel.DataAnnotations;

namespace Vidly.Contracts.Requests;

public class RentalRequest
{
	public int? CustomerId { get; init; }

	public int? MovieId { get; init; }
}