using CustomCADs.Accounts.Persistence.ShadowEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Accounts.Persistence.Configurations.ViewedProducts;

public class Configurations : IEntityTypeConfiguration<ViewedProduct>
{
	public void Configure(EntityTypeBuilder<ViewedProduct> builder)
	{
		builder
			.SetPrimaryKey()
			.SetForeignKeys()
			.SetStronglyTypedIds();
	}
}

