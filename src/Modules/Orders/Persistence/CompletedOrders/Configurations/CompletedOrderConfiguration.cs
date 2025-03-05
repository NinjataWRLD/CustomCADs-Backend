using CustomCADs.Orders.Domain.CompletedOrders;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Orders.Persistence.CompletedOrders.Configurations;

using static CompletedOrderConstants;

public class CompletedOrderConfiguration : IEntityTypeConfiguration<CompletedOrder>
{
    public void Configure(EntityTypeBuilder<CompletedOrder> builder)
        => builder
            .SetPrimaryKey()
            .SetStronglyTypedIds()
            .SetValidations();
}

public static class OrderConfigUtils
{
    public static EntityTypeBuilder<CompletedOrder> SetPrimaryKey(this EntityTypeBuilder<CompletedOrder> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<CompletedOrder> SetStronglyTypedIds(this EntityTypeBuilder<CompletedOrder> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                x => x.Value,
                v => CompletedOrderId.New(v)
            );

        builder.Property(x => x.BuyerId)
            .HasConversion(
                x => x.Value,
                v => AccountId.New(v)
            );

        builder.Property(x => x.DesignerId)
            .HasConversion(
                x => x.Value,
                v => AccountId.New(v)
            );

        builder.Property(x => x.CadId)
            .HasConversion(
                x => x.Value,
                v => CadId.New(v)
            );

        builder.Property(x => x.ShipmentId)
            .HasConversion(
                x => ShipmentId.Unwrap(x),
                v => ShipmentId.New(v)
            );
        
        builder.Property(x => x.CustomizationId)
            .HasConversion(
                x => CustomizationId.Unwrap(x),
                v => CustomizationId.New(v)
            );

        return builder;
    }

    public static EntityTypeBuilder<CompletedOrder> SetValidations(this EntityTypeBuilder<CompletedOrder> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength)
            .HasColumnName("Name");

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(DescriptionMaxLength)
            .HasColumnName("Description");

        builder.Property(x => x.Price)
            .IsRequired()
            .HasPrecision(19, 2)
            .HasColumnName("Price");

        builder.Property(x => x.Delivery)
            .IsRequired()
            .HasColumnName("Delivery");

        builder.Property(x => x.OrderDate)
            .IsRequired()
            .HasColumnName("OrderDate");

        builder.Property(x => x.PurchaseDate)
            .IsRequired()
            .HasColumnName("PurchaseDate");

        builder.Property(x => x.BuyerId)
            .IsRequired()
            .HasColumnName("BuyerId");

        builder.Property(x => x.DesignerId)
            .IsRequired()
            .HasColumnName("DesignerId");

        builder.Property(x => x.CadId)
            .IsRequired()
            .HasColumnName("CadId");

        builder.Property(x => x.ShipmentId)
            .IsRequired(false)
            .HasColumnName("ShipmentId");
        
        builder.Property(x => x.CustomizationId)
            .IsRequired(false)
            .HasColumnName("CustomizationId");

        return builder;
    }
}
