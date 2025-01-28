using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vidly.Application.Models;

namespace Vidly.Application.Data.EntityMapping;

public class MovieMapping : IEntityTypeConfiguration<Movie>
{
	public void Configure(EntityTypeBuilder<Movie> builder)
	{
		builder.Property(movie => movie.Title)
			.HasMaxLength(255)
			.IsRequired();
		
		builder.Property(movie => movie.NumberInStock)
			.HasMaxLength(255)
			.IsRequired();
		
		builder.Property(movie => movie.DailyRentalRate)
			.HasMaxLength(255)
			.IsRequired();
		
	}
}