using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vidly.Application.Models;

namespace Vidly.Application.Data.EntityMapping;

public class GenreMapping : IEntityTypeConfiguration<Genre>
{
	public void Configure(EntityTypeBuilder<Genre> builder)
	{
		builder.Property(genre => genre.Name)
			.HasMaxLength(50)
			.IsRequired();
	}
}