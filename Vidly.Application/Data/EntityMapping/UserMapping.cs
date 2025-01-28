using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vidly.Application.Models;

namespace Vidly.Application.Data.EntityMapping;

public class UserMapping : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		
		builder.Property(user => user.Name)
			.HasMaxLength(50)
			.IsRequired();
		
		builder.Property(user => user.Email)
			.HasMaxLength(255)
			.IsRequired();
		
		builder.Property(user => user.Password)
			.HasMaxLength(1024)
			.IsRequired();
		
	}
}