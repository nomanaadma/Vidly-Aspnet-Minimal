using FluentValidation;
using Vidly.Application.Data;
using Vidly.Application.Models;

namespace Vidly.Application.Repositories;

public interface IReturnRepository
{
	Task<Rental?> ReturnAsync(Return rentalReturn, CancellationToken token = default);
}

public class ReturnRepository(
	DatabaseContext context,
	IMovieRepository movieRepository,
	ICustomerRepository customerRepository,
	IRentalRepository rentalRepository,
	IValidator<Return> returnValidator
	) : IReturnRepository
{
	public async Task<Rental?> ReturnAsync(Return rentalReturn, CancellationToken token = default)
	{
		await returnValidator.ValidateAndThrowAsync(rentalReturn, token);
		
		var movie = await movieRepository.FindByIdAsync(rentalReturn.MovieId, token);
		
		var rental = await rentalRepository.FindByReturnAsync(rentalReturn, token);

		var dateNow = DateTime.UtcNow;
		
		rental!.DateReturned = dateNow;

		var rentalDays = (dateNow - rental.DateOut);

		rental.RentalFee = rentalDays.Days * movie!.DailyRentalRate;
		
		movie.NumberInStock++;
		
		context.Movies.Update(movie);
		context.Rentals.Update(rental);
		
		await context.SaveChangesAsync(token);
		
		rental.Movie = movie;
		var customer = await customerRepository.GetByIdAsync(rental.CustomerId, token);
		rental.Customer = customer!;

		return rental;

	}
}