using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Vidly.Application.Data;
using Vidly.Application.Models;

namespace Vidly.Application.Repositories;

public interface IMovieRepository : IRepository<Movie>
{
	Task<Movie?> FindByIdAsync(int id, CancellationToken token = default);
}

public class MovieRepository(
	DatabaseContext context,
	IGenreRepository genreRepository,
	IValidator<Movie> movieValidator)
	: Repository<Movie>(context, movieValidator), IMovieRepository
{
	private readonly DatabaseContext _context = context;
	private readonly IValidator<Movie> _movieValidator = movieValidator;

	public override async Task<Movie> CreateAsync(Movie movie, CancellationToken token = default)
	{
		await _movieValidator.ValidateAndThrowAsync(movie, token);
		await _context.Movies.AddAsync(movie, token);
		await _context.SaveChangesAsync(token);
		
		var genre = await genreRepository.GetByIdAsync(movie.GenreId, token);
		movie.Genre = genre!;
		
		return movie;
	}

	public override async Task<Movie?> GetByIdAsync(int id, CancellationToken token = default)
	{
		var movie = await _context.Movies.Include(m => m.Genre).FirstOrDefaultAsync(m => m.Id == id, token);
		return movie;
	}
	
	public async Task<Movie?> FindByIdAsync(int id, CancellationToken token = default)
	{
		var movie = await _context.Movies.FindAsync([id], token);
		return movie;
	}

	public override async Task<IEnumerable<Movie>> GetAllAsync(CancellationToken token = default)
	{
		var movies = await _context.Movies
			.Include(m => m.Genre)
			.OrderBy(m => m.Title)
			.ToListAsync(token);
		return movies;
	}

	public override async Task<Movie> UpdateAsync(Movie movie, CancellationToken token = default)
	{
		await _movieValidator.ValidateAndThrowAsync(movie, token);
		_context.Movies.Update(movie);
		await _context.SaveChangesAsync(token);
		
		var genre = await genreRepository.GetByIdAsync(movie.GenreId, token);
		movie.Genre = genre!;
		
		return movie;
	}
	
}
