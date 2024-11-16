using CustomCADs.Orders.Domain.Common.Enums;
using CustomCADs.Orders.Domain.CustomOrders.Entities;
using CustomCADs.Orders.Domain.CustomOrders.Enums;
using CustomCADs.Orders.Domain.CustomOrders.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Orders.Persistence.CustomOrders.Configurations;

using static CustomOrderConstants;

public class CustomOrderConfiguration : IEntityTypeConfiguration<CustomOrder>
{
    public void Configure(EntityTypeBuilder<CustomOrder> builder)
        => builder
            .SetPrimaryKey()
            .SetStronglyTypedIds()
            .SetValueObjects()
            .SetValidations();
}

public static class CustomOrderConfigUtils
{
    public static EntityTypeBuilder<CustomOrder> SetPrimaryKey(this EntityTypeBuilder<CustomOrder> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<CustomOrder> SetStronglyTypedIds(this EntityTypeBuilder<CustomOrder> builder)
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

    public static EntityTypeBuilder<CustomOrder> SetValueObjects(this EntityTypeBuilder<CustomOrder> builder)
    {
        builder.ComplexProperty(x => x.Image, a =>
        {
            a.Property(x => x.Path)
                .IsRequired()
                .HasColumnName("ImagePath");
        });

        return builder;
    }

    public static EntityTypeBuilder<CustomOrder> SetValidations(this EntityTypeBuilder<CustomOrder> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength)
            .HasColumnName("Name");

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(DescriptionMaxLength)
            .HasColumnName("Description");

        builder.Property(x => x.OrderDate)
            .IsRequired()
            .HasColumnName("OrderDate");

        builder.Property(x => x.DeliveryType)
            .IsRequired()
            .HasConversion(
                x => x.ToString(),
                s => Enum.Parse<DeliveryType>(s)
            ).HasColumnName("DeliveryType");

        builder.Property(x => x.OrderStatus)
            .IsRequired()
            .HasConversion(
                x => x.ToString(),
                s => Enum.Parse<CustomOrderStatus>(s)
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
