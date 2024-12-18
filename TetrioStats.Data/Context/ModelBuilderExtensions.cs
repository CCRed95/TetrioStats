using Microsoft.EntityFrameworkCore;

namespace TetrioStats.Data.Context;

public static class ModelBuilderExtensions
{
	public static ModelBuilder WithConfiguration<TConfiguration, TEntity>(
		this ModelBuilder @this)
		where TConfiguration : IEntityTypeConfiguration<TEntity>, new()
		where TEntity : class
	{
		@this.ApplyConfiguration(new TConfiguration());
		return @this;
	}
}