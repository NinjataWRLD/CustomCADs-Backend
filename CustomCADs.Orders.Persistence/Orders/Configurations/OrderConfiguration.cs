using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Orders.Domain.Orders.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Orders.Persistence.Orders.Configurations;

using static OrderConstants;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
        => builder
            .SetPrimaryKey()
            .SetStronglyTypedIds()
            .SetValidations();
}

public static class OrderConfigUtils
{
    public static EntityTypeBuilder<Order> SetPrimaryKey(this EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<Order> SetStronglyTypedIds(this EntityTypeBuilder<Order> builder)
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

        builder.Property(x => x.DesignerId)
            .HasConversion<Guid?>(
                x => x == null ? null : x.Value.Value,
                v => v == null ? null : new(v.Value)
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

    public static EntityTypeBuilder<Order> SetValidations(this EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength)
            .HasColumnName("Name");

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(DescriptionMaxLength)
            .HasColumnName("Description");
        
        builder.Property(x => x.Delivery)
            .IsRequired()
            .HasColumnName("Delivery");

        builder.Property(x => x.OrderDate)
            .IsRequired()
            .HasColumnName("OrderDate");

        builder.Property(x => x.OrderStatus)
            .IsRequired()
            .HasConversion(
                x => x.ToString(),
                s => Enum.Parse<OrderStatus>(s)
            ).HasColumnName("OrderStatus");

        builder.Property(x => x.BuyerId)
            .IsRequired()
            .HasColumnName("BuyerId");

        builder.Property(x => x.DesignerId)
            .HasColumnName("DesignerId");

        builder.Property(x => x.CadId)
            .HasColumnName("CadId");

        builder.Property(x => x.ShipmentId)
            .HasColumnName("ShipmentId");

        return builder;
    }
}
