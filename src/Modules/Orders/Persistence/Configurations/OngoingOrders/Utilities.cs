using CustomCADs.Orders.Domain.OngoingOrders;
using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Orders.Persistence.Configurations.OngoingOrders;

using static OngoingOrderConstants;

public static class Utilities
{
    public static EntityTypeBuilder<OngoingOrder> SetPrimaryKey(this EntityTypeBuilder<OngoingOrder> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<OngoingOrder> SetStronglyTypedIds(this EntityTypeBuilder<OngoingOrder> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                x => x.Value,
                v => OngoingOrderId.New(v)
            );

        builder.Property(x => x.BuyerId)
            .HasConversion(
                x => x.Value,
                v => AccountId.New(v)
            );

        builder.Property(x => x.DesignerId)
            .HasConversion<Guid?>(
                x => x == null ? null : x.Value.Value,
                v => AccountId.New(v)
            );

        builder.Property(x => x.CadId)
            .HasConversion<Guid?>(
                x => x == null ? null : x.Value.Value,
                v => CadId.New(v)
            );

        return builder;
    }

    public static EntityTypeBuilder<OngoingOrder> SetValidations(this EntityTypeBuilder<OngoingOrder> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength)
            .HasColumnName(nameof(OngoingOrder.Name));

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(DescriptionMaxLength)
            .HasColumnName(nameof(OngoingOrder.Description));

        builder.Property(x => x.Price)
            .HasPrecision(19, 2)
            .HasColumnName(nameof(OngoingOrder.Price));

        builder.Property(x => x.Delivery)
            .IsRequired()
            .HasColumnName(nameof(OngoingOrder.Delivery));

        builder.Property(x => x.OrderedAt)
            .IsRequired()
            .HasColumnName(nameof(OngoingOrder.OrderedAt));

        builder.Property(x => x.OrderStatus)
            .IsRequired()
            .HasConversion(
                x => x.ToString(),
                s => Enum.Parse<OngoingOrderStatus>(s)
            ).HasColumnName(nameof(OngoingOrder.OrderStatus));

        builder.Property(x => x.BuyerId)
            .IsRequired()
            .HasColumnName(nameof(OngoingOrder.BuyerId));

        builder.Property(x => x.DesignerId)
            .HasColumnName(nameof(OngoingOrder.DesignerId));

        builder.Property(x => x.CadId)
            .HasColumnName(nameof(OngoingOrder.CadId));

        return builder;
    }
}
