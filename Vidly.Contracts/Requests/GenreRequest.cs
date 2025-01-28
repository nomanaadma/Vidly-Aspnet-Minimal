using System.ComponentModel.DataAnnotations;

namespace Vidly.Contracts.Requests;

public class GenreRequest
{
	public string? Name { get; init; }
}