using CustomCADs.Orders.Domain.CompletedOrders;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Orders.Persistence.Configurations.CompletedOrders;

using static CompletedOrderConstants;

public static class Utilities
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
            .HasColumnName(nameof(CompletedOrder.Name));

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(DescriptionMaxLength)
            .HasColumnName(nameof(CompletedOrder.Description));

        builder.Property(x => x.Price)
            .IsRequired()
            .HasPrecision(19, 2)
            .HasColumnName(nameof(CompletedOrder.Price));

        builder.Property(x => x.Delivery)
            .IsRequired()
            .HasColumnName(nameof(CompletedOrder.Delivery));

        builder.Property(x => x.OrderDate)
            .IsRequired()
            .HasColumnName(nameof(CompletedOrder.OrderDate));

        builder.Property(x => x.PurchaseDate)
            .IsRequired()
            .HasColumnName(nameof(CompletedOrder.PurchaseDate));

        builder.Property(x => x.BuyerId)
            .IsRequired()
            .HasColumnName(nameof(CompletedOrder.BuyerId));

        builder.Property(x => x.DesignerId)
            .IsRequired()
            .HasColumnName(nameof(CompletedOrder.DesignerId));

        builder.Property(x => x.CadId)
            .IsRequired()
            .HasColumnName(nameof(CompletedOrder.CadId));

        builder.Property(x => x.ShipmentId)
            .IsRequired(false)
            .HasColumnName(nameof(CompletedOrder.ShipmentId));

        builder.Property(x => x.CustomizationId)
            .IsRequired(false)
            .HasColumnName(nameof(CompletedOrder.CustomizationId));

        return builder;
    }
}
