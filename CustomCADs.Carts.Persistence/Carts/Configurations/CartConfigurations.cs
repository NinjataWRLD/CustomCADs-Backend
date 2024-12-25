using CustomCADs.Carts.Domain.Carts;
using CustomCADs.Carts.Domain.Carts.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Carts.Persistence.Carts.Configurations;

public class CartConfigurations : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
        => builder
            .SetPrimaryKey()
            .SetForeignKeys()
            .SetStronglyTypedIds()
            .SetValidations();
}

public static class CartItemConfigurUitls
{
    public static EntityTypeBuilder<Cart> SetPrimaryKey(this EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<Cart> SetForeignKeys(this EntityTypeBuilder<Cart> builder)
    {
        builder
            .HasMany(x => x.Items)
            .WithOne(x => x.Cart)
            .HasForeignKey(x => x.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        return builder;
    }

    public static EntityTypeBuilder<Cart> SetStronglyTypedIds(this EntityTypeBuilder<Cart> builder)
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

    public static EntityTypeBuilder<Cart> SetValidations(this EntityTypeBuilder<Cart> builder)
    {
        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion(
                x => x.ToString(),
                s => Enum.Parse<CartStatus>(s)
            )
            .HasColumnName("Status");

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
