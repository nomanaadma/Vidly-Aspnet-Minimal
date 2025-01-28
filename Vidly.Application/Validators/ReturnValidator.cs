using FluentValidation;
using FluentValidation.Results;
using Vidly.Application.Models;
using Vidly.Application.Repositories;

namespace Vidly.Application.Validators;

public class ReturnValidator : AbstractValidator<Return> 
{
	private readonly IRentalRepository _rentalRepository;
	
	public ReturnValidator(IRentalRepository rentalRepository)
	{
		_rentalRepository = rentalRepository;

		RuleFor(r => r)
			.CustomAsync(ValidateRental);
	}

	private async Task ValidateRental(Return rentalReturn, ValidationContext<Return> validationContext, CancellationToken token = default)
	{
		var rental = await _rentalRepository.FindByReturnAsync(rentalReturn, token);

		switch (rental)
		{
			case null:
				validationContext.AddFailure( new ValidationFailure("Rental", "Not found."));
				break;
			case { DateReturned: not null }:
				validationContext.AddFailure(new ValidationFailure("Rental", "Already processed."));
				break;
		}
	}
	
}