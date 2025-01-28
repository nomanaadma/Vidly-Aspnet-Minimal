using System.ComponentModel.DataAnnotations;

namespace Vidly.Contracts.Requests;

public class CustomerRequest
{
	[Required]
	public string? Name { get; init; }
	
	[Required]
	public string? Phone { get; init; }

	[Required]
	public bool? IsGold { get; init; }
}