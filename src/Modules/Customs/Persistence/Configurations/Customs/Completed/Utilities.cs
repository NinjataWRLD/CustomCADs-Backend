using CustomCADs.Customs.Domain.Customs;
using CustomCADs.Customs.Domain.Customs.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Customs;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Customs.Persistence.Configurations.Customs.Completed;

public static class Utilities
{
    public static EntityTypeBuilder<CompletedCustom> SetPrimaryKey(this EntityTypeBuilder<CompletedCustom> builder)
    {
        builder.HasKey(x => x.CustomId);

        return builder;
    }

    public static EntityTypeBuilder<CompletedCustom> SetStronglyTypedIds(this EntityTypeBuilder<CompletedCustom> builder)
    {
        builder.Property(x => x.CustomId)
            .HasConversion(
                x => x.Value,
                v => CustomId.New(v)
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

    public static EntityTypeBuilder<CompletedCustom> SetNavigations(this EntityTypeBuilder<CompletedCustom> builder)
    {
        builder
            .HasOne<Custom>()
            .WithOne(x => x.CompletedCustom)
            .HasForeignKey<CompletedCustom>(x => x.CustomId);

        return builder;
    }

    public static EntityTypeBuilder<CompletedCustom> SetValidations(this EntityTypeBuilder<CompletedCustom> builder)
    {
        builder.Property(x => x.ShipmentId)
            .HasColumnName(nameof(CompletedCustom.ShipmentId));

        builder.Property(x => x.CustomizationId)
            .HasColumnName(nameof(CompletedCustom.CustomizationId));

        return builder;
    }
}
