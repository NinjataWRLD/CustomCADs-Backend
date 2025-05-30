using CustomCADs.Categories.Domain.Categories;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Categories.Persistence.Configurations.Categories;

public class Configurations : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder
			.SetPrimaryKey()
			.SetStronglyTypedIds()
			.SetValidations()
			.SetSeedData();
	}
}
