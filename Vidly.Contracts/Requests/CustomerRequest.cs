using System.ComponentModel.DataAnnotations;

namespace Vidly.Contracts.Requests;

public class CustomerRequest
{
	public string? Name { get; init; }
	
	public string? Phone { get; init; }

	public bool? IsGold { get; init; }
}