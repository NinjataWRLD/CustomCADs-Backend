using CustomCADs.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Carts.Persistence.PurchasedCarts.Configurations;

public class PurchasedCartItemConfiguration : IEntityTypeConfiguration<PurchasedCartItem>
{
    public void Configure(EntityTypeBuilder<PurchasedCartItem> builder)
        => builder
            .SetPrimaryKey()
            .SetForeignKeys()
            .SetStronglyTypedIds()
            .SetValidations();
}

public static class PurchasedCartItemItemConfigUtils
{
    public static EntityTypeBuilder<PurchasedCartItem> SetPrimaryKey(this EntityTypeBuilder<PurchasedCartItem> builder)
    {
        builder.HasKey(x => x.ProductId);

        return builder;
    }

    public static EntityTypeBuilder<PurchasedCartItem> SetForeignKeys(this EntityTypeBuilder<PurchasedCartItem> builder)
    {
        builder
            .HasOne(x => x.Cart)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        return builder;
    }

    public static EntityTypeBuilder<PurchasedCartItem> SetStronglyTypedIds(this EntityTypeBuilder<PurchasedCartItem> builder)
    {
        builder.Property(x => x.CartId)
            .HasConversion(
                x => x.Value,
                v => PurchasedCartId.New(v)
            );

        builder.Property(x => x.ProductId)
            .HasConversion(
                x => x.Value,
                v => ProductId.New(v)
            );

        builder.Property(x => x.CadId)
            .HasConversion(
                x => x.Value,
                v => CadId.New(v)
            );
        
        builder.Property(x => x.CustomizationId)
            .HasConversion(
                x => CustomizationId.Unwrap(x),
                v => CustomizationId.New(v)
            );

        return builder;
    }

    public static EntityTypeBuilder<PurchasedCartItem> SetValidations(this EntityTypeBuilder<PurchasedCartItem> builder)
    {
        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasColumnName("Quantity");

        builder.Property(x => x.Price)
            .IsRequired()
            .HasPrecision(10, 2)
            .HasColumnName("Price");

        builder.Property(x => x.ForDelivery)
            .IsRequired()
            .HasColumnName("ForDelivery");

        builder.Property(x => x.ProductId)
            .IsRequired()
            .HasColumnName("ProductId");

        builder.Property(x => x.CartId)
            .IsRequired()
            .HasColumnName("CartId");

        builder.Property(x => x.CadId)
            .IsRequired()
            .HasColumnName("CadId");
        
        builder.Property(x => x.CustomizationId)
            .IsRequired(false)
            .HasColumnName("CustomizationId");

        return builder;
    }
}
