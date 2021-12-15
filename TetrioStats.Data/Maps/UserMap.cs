using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TetrioStats.Data.Domain;

namespace TetrioStats.Data.Maps
{
	public class UserMap
		: IEntityTypeConfiguration<User>
	{
		public void Configure(
			EntityTypeBuilder<User> builder)
		{
			builder.ToTable("Users");

			builder.HasKey(t => t.UserID);
			builder
				.Property(t => t.UserID)
				.IsRequired()
				.ValueGeneratedOnAdd();

			builder
				.Property(t => t.TetrioUserID)
				.HasMaxLength(24)
				.IsFixedLength()
				.IsRequired();

			builder
				.Property(t => t.Username)
				.HasMaxLength(16)
				.IsRequired();

			builder
				.HasIndex(t => t.TetrioUserID)
				.IsUnique();

			builder
				.HasIndex(t => t.Username)
				.IsUnique();
		}
	}
}