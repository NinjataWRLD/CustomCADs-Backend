using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Carts.Persistence.ActiveCarts.Configurations;

public class ActiveCartItemConfiguration : IEntityTypeConfiguration<ActiveCartItem>
{
    public void Configure(EntityTypeBuilder<ActiveCartItem> builder)
        => builder
            .SetPrimaryKey()
            .SetForeignKeys()
            .SetStronglyTypedIds()
            .SetValidations();
}

public static class ActiveCartItemItemConfigUtils
{
    public static EntityTypeBuilder<ActiveCartItem> SetPrimaryKey(this EntityTypeBuilder<ActiveCartItem> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<ActiveCartItem> SetForeignKeys(this EntityTypeBuilder<ActiveCartItem> builder)
    {
        builder
            .HasOne(x => x.Cart)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        return builder;
    }

    public static EntityTypeBuilder<ActiveCartItem> SetStronglyTypedIds(this EntityTypeBuilder<ActiveCartItem> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                x => x.Value,
                v => ActiveCartItemId.New(v)
            );

        builder.Property(x => x.CartId)
            .HasConversion(
                x => x.Value,
                v => ActiveCartId.New(v)
            );

        builder.Property(x => x.ProductId)
            .HasConversion(
                x => x.Value,
                v => ProductId.New(v)
            );

        return builder;
    }

    public static EntityTypeBuilder<ActiveCartItem> SetValidations(this EntityTypeBuilder<ActiveCartItem> builder)
    {
        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasColumnName("Quantity");

        builder.Property(x => x.Weight)
            .IsRequired()
            .HasPrecision(6, 2)
            .HasColumnName("Weight");

        builder.Property(x => x.ForDelivery)
            .IsRequired()
            .HasColumnName("ForDelivery");

        builder.Property(x => x.ProductId)
            .IsRequired()
            .HasColumnName("ProductId");

        builder.Property(x => x.CartId)
            .IsRequired()
            .HasColumnName("CartId");

        return builder;
    }
}
