using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TetrioStats.Data.Domain;
using TetrioStats.Data.Seeding;

namespace TetrioStats.Data.Maps;

public class CountryMap
	: IEntityTypeConfiguration<Country>
{
	public void Configure(
		EntityTypeBuilder<Country> builder)
	{
		builder.ToTable("Countries");

		builder.HasKey(t => t.CountryID);
		builder
			.Property(t => t.CountryID)
			.IsRequired()
			.ValueGeneratedOnAdd();

		builder
			.Property(t => t.Name)
			.IsRequired();

		builder
			.Property(t => t.CountryCode)
			.IsRequired();

		builder
			.Property(t => t.NumericCode)
			.IsRequired();

		builder
			.HasIndex(t => t.Name)
			.IsUnique();

		builder
			.HasIndex(t => t.CountryCode)
			.IsUnique();

		builder
			.HasIndex(t => t.NumericCode)
			.IsUnique();

		var dataSeeder = new CountryCodesSeeder();
			
		var data = dataSeeder
			.GetCountries()
			.ToArray();

		builder
			.HasData(data);
	}
}