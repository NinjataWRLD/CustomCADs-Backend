using CustomCADs.Orders.Domain.OngoingOrders;
using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Orders.Persistence.OngoingOrders.Configurations;

using static OngoingOrderConstants;

public class OngoingOrderConfiguration : IEntityTypeConfiguration<OngoingOrder>
{
    public void Configure(EntityTypeBuilder<OngoingOrder> builder)
        => builder
            .SetPrimaryKey()
            .SetStronglyTypedIds()
            .SetValidations();
}

public static class OrderConfigUtils
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
            .HasColumnName("Name");

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(DescriptionMaxLength)
            .HasColumnName("Description");
        
        builder.Property(x => x.Price)
            .HasPrecision(19, 2)
            .HasColumnName("Price");

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
                s => Enum.Parse<OngoingOrderStatus>(s)
            ).HasColumnName("OrderStatus");

        builder.Property(x => x.BuyerId)
            .IsRequired()
            .HasColumnName("BuyerId");

        builder.Property(x => x.DesignerId)
            .HasColumnName("DesignerId");

        builder.Property(x => x.CadId)
            .HasColumnName("CadId");

        return builder;
    }
}
