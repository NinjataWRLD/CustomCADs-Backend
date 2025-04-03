using CustomCADs.Carts.Domain.PurchasedCarts;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Carts.Persistence.Configurations.PurchasedCarts;

public static class Utilities
{
    public static EntityTypeBuilder<PurchasedCart> SetPrimaryKey(this EntityTypeBuilder<PurchasedCart> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<PurchasedCart> SetForeignKeys(this EntityTypeBuilder<PurchasedCart> builder)
    {
        builder
            .HasMany(x => x.Items)
            .WithOne(x => x.Cart)
            .HasForeignKey(x => x.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        return builder;
    }

    public static EntityTypeBuilder<PurchasedCart> SetStronglyTypedIds(this EntityTypeBuilder<PurchasedCart> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                x => x.Value,
                v => PurchasedCartId.New(v)
            );

        builder.Property(x => x.BuyerId)
            .HasConversion(
                x => x.Value,
                v => AccountId.New(v)
            );

        builder.Property(x => x.ShipmentId)
            .HasConversion(
                x => ShipmentId.Unwrap(x),
                v => ShipmentId.New(v)
            );

        return builder;
    }

    public static EntityTypeBuilder<PurchasedCart> SetValidations(this EntityTypeBuilder<PurchasedCart> builder)
    {
        builder.Property(x => x.PurchasedAt)
            .IsRequired()
            .HasColumnName(nameof(PurchasedCart.PurchasedAt));

        builder.Property(x => x.BuyerId)
            .IsRequired()
            .HasColumnName(nameof(PurchasedCart.BuyerId));

        builder.Property(x => x.ShipmentId)
            .HasColumnName(nameof(PurchasedCart.ShipmentId));

        return builder;
    }
}
