using CustomCADs.Orders.Domain.Carts.Entities;
using CustomCADs.Orders.Domain.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Orders.Persistence.Carts.Configurations;

public class GalleryOrderConfiguration : IEntityTypeConfiguration<GalleryOrder>
{
    public void Configure(EntityTypeBuilder<GalleryOrder> builder)
        => builder
            .SetPrimaryKey()
            .SetForeignKeys()
            .SetStronglyTypedIds()
            .SetValueObjects()
            .SetValidations();
}

public static class GalleryOrderItemConfigUtils
{
    public static EntityTypeBuilder<GalleryOrder> SetPrimaryKey(this EntityTypeBuilder<GalleryOrder> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<GalleryOrder> SetForeignKeys(this EntityTypeBuilder<GalleryOrder> builder)
    {
        builder
            .HasOne(x => x.Cart)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        return builder;
    }

    public static EntityTypeBuilder<GalleryOrder> SetStronglyTypedIds(this EntityTypeBuilder<GalleryOrder> builder)
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

    public static EntityTypeBuilder<GalleryOrder> SetValueObjects(this EntityTypeBuilder<GalleryOrder> builder)
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

    public static EntityTypeBuilder<GalleryOrder> SetValidations(this EntityTypeBuilder<GalleryOrder> builder)
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
