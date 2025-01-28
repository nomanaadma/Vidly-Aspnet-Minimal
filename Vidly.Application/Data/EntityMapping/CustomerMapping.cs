using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vidly.Application.Models;

namespace Vidly.Application.Data.EntityMapping;

public class CustomerMapping : IEntityTypeConfiguration<Customer>
{
	public void Configure(EntityTypeBuilder<Customer> builder)
	{
		builder.Property(customer => customer.Name)
			.HasMaxLength(50)
			.IsRequired();

		builder.Property(customer => customer.IsGold)
			.HasDefaultValue(false);
		
		builder.Property(customer => customer.Phone)
			.HasMaxLength(50)
			.IsRequired();
	}
}