using CustomCADs.Carts.Domain.PurchasedCarts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Carts.Persistence.PurchasedCarts.Configurations;

public class PurchasedCartConfigurations : IEntityTypeConfiguration<PurchasedCart>
{
    public void Configure(EntityTypeBuilder<PurchasedCart> builder)
        => builder
            .SetPrimaryKey()
            .SetForeignKeys()
            .SetStronglyTypedIds()
            .SetValidations();
}

public static class PurchasedCartItemConfigurUitls
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
                v => new(v)
            );

        builder.Property(x => x.BuyerId)
            .HasConversion(
                x => x.Value,
                v => new(v)
            );

        builder.Property(x => x.ShipmentId)
            .HasConversion<Guid?>(
                x => x == null ? null : x.Value.Value,
                v => v == null ? null : new(v.Value)
            );

        return builder;
    }

    public static EntityTypeBuilder<PurchasedCart> SetValidations(this EntityTypeBuilder<PurchasedCart> builder)
    {
        builder.Property(x => x.PurchaseDate)
            .IsRequired()
            .HasColumnName("PurchaseDate");

        builder.Property(x => x.BuyerId)
            .IsRequired()
            .HasColumnName("BuyerId");

        builder.Property(x => x.ShipmentId)
            .HasColumnName("ShipmentId");

        return builder;
    }
}
