using CustomCADs.Accounts.Persistence.ShadowEntities;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Accounts.Persistence.Configurations.ViewedProducts;

static class Utilities
{
	public static EntityTypeBuilder<ViewedProduct> SetPrimaryKey(this EntityTypeBuilder<ViewedProduct> builder)
	{
		builder.HasKey(x => new { x.AccountId, x.ProductId });

		return builder;
	}

	public static EntityTypeBuilder<ViewedProduct> SetForeignKeys(this EntityTypeBuilder<ViewedProduct> builder)
	{
		builder
			.HasOne(x => x.Account)
			.WithMany()
			.HasForeignKey(x => x.AccountId)
			.OnDelete(DeleteBehavior.Cascade);

		return builder;
	}

	public static EntityTypeBuilder<ViewedProduct> SetStronglyTypedIds(this EntityTypeBuilder<ViewedProduct> builder)
	{
		builder.Property(pt => pt.AccountId)
			.HasConversion(
				id => id.Value,
				val => AccountId.New(val)
			);

		builder.Property(pt => pt.ProductId)
			.HasConversion(
				id => id.Value,
				val => ProductId.New(val)
			);

		return builder;
	}
}
