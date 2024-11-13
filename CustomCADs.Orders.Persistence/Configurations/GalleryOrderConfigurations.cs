using CustomCADs.Orders.Domain.Common.Enums;
using CustomCADs.Orders.Domain.GalleryOrders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Orders.Persistence.Configurations;

public class GalleryOrderConfigurations : IEntityTypeConfiguration<GalleryOrder>
{
    public void Configure(EntityTypeBuilder<GalleryOrder> builder)
        => builder
            .SetPrimaryKey()
            .SetForeignKeys()
            .SetStronglyTypedIds()
            .SetValidations();
}

public static class GalleryOrderConfigurUitls
{
    public static EntityTypeBuilder<GalleryOrder> SetPrimaryKey(this EntityTypeBuilder<GalleryOrder> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }
    
    public static EntityTypeBuilder<GalleryOrder> SetForeignKeys(this EntityTypeBuilder<GalleryOrder> builder)
    {
        builder
            .HasMany(x => x.Items)
            .WithOne(x => x.GalleryOrder)
            .HasForeignKey(x => x.GalleryOrderId)
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

        builder.Property(x => x.BuyerId)
            .HasConversion(
                x => x.Value,
                v => new(v)
            );

        return builder;
    }
    
    public static EntityTypeBuilder<GalleryOrder> SetValidations(this EntityTypeBuilder<GalleryOrder> builder)
    {
        builder.Property(x => x.Total)
            .IsRequired()
            .HasPrecision(18, 2)
            .HasColumnName("Total");
        
        builder.Property(x => x.DeliveryType)
            .IsRequired()
            .HasConversion(
                x => x.ToString(),
                s => Enum.Parse<DeliveryType>(s)
            ).HasColumnName("DeliveryType");
        
        builder.Property(x => x.PurchaseDate)
            .IsRequired()
            .HasColumnName("PurchaseDate");
        
        builder.Property(x => x.BuyerId)
            .IsRequired()
            .HasColumnName("BuyerId");

        return builder;
    }
}
