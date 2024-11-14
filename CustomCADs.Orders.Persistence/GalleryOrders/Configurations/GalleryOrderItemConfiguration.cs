using CustomCADs.Orders.Domain.GalleryOrders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Orders.Persistence.GalleryOrders.Configurations;

public class GalleryOrderItemConfiguration : IEntityTypeConfiguration<GalleryOrderItem>
{
    public void Configure(EntityTypeBuilder<GalleryOrderItem> builder)
        => builder
            .SetPrimaryKey()
            .SetForeignKeys()
            .SetStronglyTypedIds()
            .SetValueObjects()
            .SetValidations();
}

public static class GalleryOrderItemConfigUtils
{
    public static EntityTypeBuilder<GalleryOrderItem> SetPrimaryKey(this EntityTypeBuilder<GalleryOrderItem> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<GalleryOrderItem> SetForeignKeys(this EntityTypeBuilder<GalleryOrderItem> builder)
    {
        builder
            .HasOne(x => x.GalleryOrder)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.GalleryOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        return builder;
    }

    public static EntityTypeBuilder<GalleryOrderItem> SetStronglyTypedIds(this EntityTypeBuilder<GalleryOrderItem> builder)
    {
        builder.Property(x => x.Id)
            .HasConversion(
                x => x.Value,
                v => new(v)
            );

        builder.Property(x => x.GalleryOrderId)
            .HasConversion(
                x => x.Value,
                v => new(v)
            );

        builder.Property(x => x.ProductId)
            .HasConversion(
                x => x.Value,
                v => new(v)
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

    public static EntityTypeBuilder<GalleryOrderItem> SetValueObjects(this EntityTypeBuilder<GalleryOrderItem> builder)
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

    public static EntityTypeBuilder<GalleryOrderItem> SetValidations(this EntityTypeBuilder<GalleryOrderItem> builder)
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

        builder.Property(x => x.GalleryOrderId)
            .IsRequired()
            .HasColumnName("GalleryOrderId");

        builder.Property(x => x.CadId)
            .HasColumnName("CadId");

        builder.Property(x => x.ShipmentId)
            .HasColumnName("ShipmentId");

        return builder;
    }
}
