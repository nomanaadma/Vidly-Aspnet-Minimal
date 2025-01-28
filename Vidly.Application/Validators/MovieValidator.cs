using FluentValidation;
using Vidly.Application.Models;
using Vidly.Application.Repositories;

namespace Vidly.Application.Validators;

public class MovieValidator : AbstractValidator<Movie>
{
	private readonly IGenreRepository _genreRepository;
	
	public MovieValidator(IGenreRepository genreRepository)
	{

		_genreRepository = genreRepository;
		
		RuleFor(m => m.Title)
			.Length(5, 255)
			.NotEmpty();
		
		RuleFor(m => m.GenreId)
			.MustAsync(ValidateGenre)
			.WithMessage("Invalid Genre");
		
		RuleFor(m => m.NumberInStock)
			.NotNull();
		
		RuleFor(m => m.DailyRentalRate)
			.NotNull();
	
	}
	
	private async Task<bool> ValidateGenre(Movie movie, int genreId, CancellationToken token = default)
	{
		var existingGenre = await _genreRepository.GetByIdAsync(genreId, token);
		return existingGenre is not null;
	}
}