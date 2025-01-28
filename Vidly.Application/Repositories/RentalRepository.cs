using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Vidly.Application.Data;
using Vidly.Application.Models;

namespace Vidly.Application.Repositories;

public class RentalRepository(
	DatabaseContext context,
	IMovieRepository movieRepository,
	ICustomerRepository customerRepository,
	IValidator<Rental> rentalValidator
	) 
	: IRentalRepository
{
	public async Task<Rental> CreateAsync(Rental rental, CancellationToken token = default)
	{
		await rentalValidator.ValidateAndThrowAsync(rental, token);
		
		var movie = await movieRepository.FindByIdAsync(rental.MovieId, token);
		
		movie!.NumberInStock--;
		
		context.Movies.Update(movie);
		
		await context.Rentals.AddAsync(rental, token);
		
		await context.SaveChangesAsync(token);
		
		rental.Movie = movie;
		
		var customer = await customerRepository.GetByIdAsync(rental.CustomerId, token);
		rental.Customer = customer!;
			
		return rental;
	}

	public async Task<Rental?> GetByIdAsync(int id, CancellationToken token = default)
	{
		var rental = await context.Rentals
			.Include(r => r.Customer)
			.Include(r => r.Movie)
			.FirstOrDefaultAsync(r => r.Id == id, token);
		
		return rental;
	}

	public async Task<IEnumerable<Rental>> GetAllAsync(CancellationToken token = default)
	{
		var rentals = await context.Rentals
			.Include(r => r.Customer)
			.Include(r => r.Movie)
			.OrderByDescending(r => r.DateOut).ToListAsync(token);
		return rentals;
	}

	public async Task<Rental?> FindByReturnAsync(Return rentalReturn, CancellationToken token = default)
	{
		var rental = await context.Rentals.FirstOrDefaultAsync(
			r => r.CustomerId == rentalReturn.CustomerId && r.MovieId == rentalReturn.MovieId, 
			token);
		return rental;
	}
}
