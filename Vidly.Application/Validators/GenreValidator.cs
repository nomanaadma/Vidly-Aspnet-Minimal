using FluentValidation;
using Vidly.Application.Models;

namespace Vidly.Application.Validators;

public class GenreValidator : AbstractValidator<Genre> 
{
	public GenreValidator()
	{
		RuleFor(g => g.Name)
			.Length(5, 50)
			.NotEmpty();
		
	}
}