using CustomCADs.Delivery.Domain.Shipments;
using CustomCADs.Delivery.Domain.Shipments.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Delivery.Persistence.Shipments.Configurations;

public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
{
    public void Configure(EntityTypeBuilder<Shipment> builder)
       => builder
            .SetPrimaryKey()
            .SetStronglyTypedIds()
            .SetValueObjects()
            .SetValidations()
        ;
}

public static class ShipmentConfigUtils
{
    public static EntityTypeBuilder<Shipment> SetPrimaryKey(this EntityTypeBuilder<Shipment> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<Shipment> SetStronglyTypedIds(this EntityTypeBuilder<Shipment> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                x => x.Value,
                v => new(v)
            );

        builder.Property(x => x.BuyerId)
            .ValueGeneratedOnAdd()
            .HasConversion(
                x => x.Value,
                v => new(v)
            );

        return builder;
    }

    public static EntityTypeBuilder<Shipment> SetValueObjects(this EntityTypeBuilder<Shipment> builder)
    {
        builder.ComplexProperty(x => x.Address, a =>
        {
            a.Property(x => x.Street)
                .IsRequired()
                .HasColumnName("Street");

            a.Property(x => x.City)
                .IsRequired()
                .HasColumnName("City");

            a.Property(x => x.Country)
                .IsRequired()
                .HasColumnName("Country");
        });

        return builder;
    }

    public static EntityTypeBuilder<Shipment> SetValidations(this EntityTypeBuilder<Shipment> builder)
    {
        builder.Property(x => x.ShipmentStatus)
            .IsRequired()
            .HasConversion(
                x => x.ToString(),
                s => Enum.Parse<ShipmentStatus>(s)
            ).HasColumnName("Status"); ;

        builder.Property(x => x.BuyerId)
            .IsRequired()
            .HasColumnName("BuyerId"); ;

        return builder;
    }
}
