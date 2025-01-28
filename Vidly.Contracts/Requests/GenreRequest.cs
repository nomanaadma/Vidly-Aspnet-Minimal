using System.ComponentModel.DataAnnotations;

namespace Vidly.Contracts.Requests;

public class GenreRequest
{
	[Required]
	public string? Name { get; init; }
}