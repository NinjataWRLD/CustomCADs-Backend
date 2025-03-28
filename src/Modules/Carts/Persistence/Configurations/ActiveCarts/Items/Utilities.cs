﻿using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Carts.Persistence.Configurations.ActiveCarts.Items;

public static class Utilities
{
    public static EntityTypeBuilder<ActiveCartItem> SetPrimaryKey(this EntityTypeBuilder<ActiveCartItem> builder)
    {
        builder.HasKey(x => x.ProductId);

        return builder;
    }

    public static EntityTypeBuilder<ActiveCartItem> SetForeignKeys(this EntityTypeBuilder<ActiveCartItem> builder)
    {
        builder
            .HasOne(x => x.Cart)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        return builder;
    }

    public static EntityTypeBuilder<ActiveCartItem> SetStronglyTypedIds(this EntityTypeBuilder<ActiveCartItem> builder)
    {
        builder.Property(x => x.CartId)
            .HasConversion(
                x => x.Value,
                v => ActiveCartId.New(v)
            );

        builder.Property(x => x.ProductId)
            .HasConversion(
                x => x.Value,
                v => ProductId.New(v)
            );

        builder.Property(x => x.CustomizationId)
            .HasConversion(
                x => CustomizationId.Unwrap(x),
                v => CustomizationId.New(v)
            );

        return builder;
    }

    public static EntityTypeBuilder<ActiveCartItem> SetValidations(this EntityTypeBuilder<ActiveCartItem> builder)
    {
        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasColumnName(nameof(ActiveCartItem.Quantity));

        builder.Property(x => x.ForDelivery)
            .IsRequired()
            .HasColumnName(nameof(ActiveCartItem.ForDelivery));

        builder.Property(x => x.ProductId)
            .IsRequired()
            .HasColumnName(nameof(ActiveCartItem.ProductId));

        builder.Property(x => x.CartId)
            .IsRequired()
            .HasColumnName(nameof(ActiveCartItem.CartId));

        builder.Property(x => x.CustomizationId)
            .IsRequired(false)
            .HasColumnName(nameof(ActiveCartItem.CustomizationId));

        return builder;
    }
}
