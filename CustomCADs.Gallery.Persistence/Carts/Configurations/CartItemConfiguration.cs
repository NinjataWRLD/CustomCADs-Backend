using CustomCADs.Gallery.Domain.Carts.Entities;
using CustomCADs.Gallery.Domain.Carts.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Gallery.Persistence.Carts.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
        => builder
            .SetPrimaryKey()
            .SetForeignKeys()
            .SetStronglyTypedIds()
            .SetValueObjects()
            .SetValidations();
}

public static class CartItemItemConfigUtils
{
    public static EntityTypeBuilder<CartItem> SetPrimaryKey(this EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<CartItem> SetForeignKeys(this EntityTypeBuilder<CartItem> builder)
    {
        builder
            .HasOne(x => x.Cart)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        return builder;
    }

    public static EntityTypeBuilder<CartItem> SetStronglyTypedIds(this EntityTypeBuilder<CartItem> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                x => x.Value,
                v => new(v)
            );

        builder.Property(x => x.CartId)
            .HasConversion(
                x => x.Value,
                v => new(v)
            );

        builder.Property(x => x.ProductId)
            .HasConversion(
                x => x.Value,
                v => new(v)
            );

        builder.Property(x => x.DeliveryType)
            .IsRequired()
            .HasConversion(
                x => x.ToString(),
                s => Enum.Parse<DeliveryType>(s)
            );

        builder.Property(x => x.CadId)
            .HasConversion<Guid?>(
                x => x == null ? null : x.Value.Value,
                v => v == null ? null : new(v.Value)
            );

        builder.Property(x => x.ShipmentId)
            .HasConversion<Guid?>(
                x => x == null ? null : x.Value.Value,
                v => v == null ? null : new(v.Value)
            );

        return builder;
    }

    public static EntityTypeBuilder<CartItem> SetValueObjects(this EntityTypeBuilder<CartItem> builder)
    {
        builder.ComplexProperty(x => x.Price, a =>
        {
            a.Property(x => x.Amount)
                .IsRequired()
                .HasPrecision(18, 2).HasColumnName("PriceAmount");

            a.Property(x => x.Precision)
                .IsRequired()
                .HasColumnName("PricePrecision");

            a.Property(x => x.Currency)
                .IsRequired()
                .HasColumnName("PriceCurrency");

            a.Property(x => x.Symbol)
                .IsRequired()
                .HasColumnName("PriceSymbol");
        });

        return builder;
    }

    public static EntityTypeBuilder<CartItem> SetValidations(this EntityTypeBuilder<CartItem> builder)
    {
        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasColumnName("Quantity");

        builder.Property(x => x.PurchaseDate)
            .IsRequired()
            .HasColumnName("PurchaseDate");

        builder.Property(x => x.ProductId)
            .IsRequired()
            .HasColumnName("ProductId");

        builder.Property(x => x.CartId)
            .IsRequired()
            .HasColumnName("CartId");

        builder.Property(x => x.CadId)
            .HasColumnName("CadId");

        builder.Property(x => x.ShipmentId)
            .HasColumnName("ShipmentId");

        return builder;
    }
}
