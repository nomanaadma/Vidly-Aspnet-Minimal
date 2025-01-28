using FluentValidation;
using Vidly.Application.Models;

namespace Vidly.Application.Validators;

public class CustomerValidator : AbstractValidator<Customer> 
{
	public CustomerValidator()
	{
		RuleFor(c => c.Name)
			.Length(5, 50)
			.NotEmpty();
		
		RuleFor(c => c.Phone)
			.Length(5, 50)
			.NotEmpty();
		
	}
}