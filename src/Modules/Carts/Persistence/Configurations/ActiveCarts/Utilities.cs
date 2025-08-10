using CustomCADs.Carts.Domain.ActiveCarts;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Printing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Carts.Persistence.Configurations.ActiveCarts;

public static class Utilities
{
	public static EntityTypeBuilder<ActiveCartItem> SetPrimaryKey(this EntityTypeBuilder<ActiveCartItem> builder)
	{
		builder.HasKey(x => new { x.ProductId, x.BuyerId });

		return builder;
	}

	public static EntityTypeBuilder<ActiveCartItem> SetStronglyTypedIds(this EntityTypeBuilder<ActiveCartItem> builder)
	{
		builder.Property(x => x.BuyerId)
			.HasConversion(
				x => x.Value,
				v => AccountId.New(v)
			);

		builder.Property(x => x.ProductId)
			.HasConversion(
				x => x.Value,
				v => ProductId.New(v)
			);

		builder.Property(x => x.CustomizationId)
			.HasConversion(
				x => CustomizationId.Unwrap(x),
				v => CustomizationId.New(v)
			);

		return builder;
	}

	public static EntityTypeBuilder<ActiveCartItem> SetValidations(this EntityTypeBuilder<ActiveCartItem> builder)
	{
		builder.Property(x => x.BuyerId)
			.IsRequired()
			.HasColumnName(nameof(ActiveCartItem.BuyerId));

		builder.Property(x => x.Quantity)
			.IsRequired()
			.HasColumnName(nameof(ActiveCartItem.Quantity));

		builder.Property(x => x.ForDelivery)
			.IsRequired()
			.HasColumnName(nameof(ActiveCartItem.ForDelivery));

		builder.Property(x => x.AddedAt)
			.IsRequired()
			.HasColumnName(nameof(ActiveCartItem.AddedAt));

		builder.Property(x => x.ProductId)
			.IsRequired()
			.HasColumnName(nameof(ActiveCartItem.ProductId));

		builder.Property(x => x.CustomizationId)
			.IsRequired(false)
			.HasColumnName(nameof(ActiveCartItem.CustomizationId));

		return builder;
	}
}
