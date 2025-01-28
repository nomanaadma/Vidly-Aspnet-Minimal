using FluentValidation;
using Vidly.Application.Models;
using Vidly.Application.Repositories;

namespace Vidly.Application.Validators;

public class RentalValidator : AbstractValidator<Rental> 
{
	private readonly IMovieRepository _movieRepository;
	private readonly ICustomerRepository _customerRepository;
	
	public RentalValidator(IMovieRepository movieRepository, ICustomerRepository customerRepository)
	{
		_movieRepository = movieRepository;
		_customerRepository = customerRepository;

		RuleFor(r => r.MovieId)
			.CustomAsync(ValidateMovie);
		
		RuleFor(r => r.CustomerId)
			.MustAsync(ValidateCustomer)
			.WithMessage("Invalid Customer")
			.NotEmpty();
		
	}

	private async Task ValidateMovie(int movieId, ValidationContext<Rental> validationContext, CancellationToken token = default)
	{
		var movie = await _movieRepository.FindByIdAsync(movieId, token);
		
		switch (movie)
		{
			case null:
				validationContext.AddFailure("Invalid Movie.");
				break;
			case { NumberInStock: 0 }:
				validationContext.AddFailure("Movie not in stock.");
				break;
		}
	}
	
	private async Task<bool> ValidateCustomer(Rental rental, int customerId, CancellationToken token = default)
	{
		var existingCustomer = await _customerRepository.GetByIdAsync(customerId, token);
		return existingCustomer is not null;
	}
	
}