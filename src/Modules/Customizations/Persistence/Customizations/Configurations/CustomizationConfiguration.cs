using CustomCADs.Customizations.Domain.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Customizations.Persistence.Customizations.Configurations;

public class CustomizationConfiguration : IEntityTypeConfiguration<Customization>
{
    public void Configure(EntityTypeBuilder<Customization> builder)
    {
        builder
            .SetPrimaryKey()
            .SetStronglyTypedIds()
            .SetValidations();
    }
}

static class CustomizationConfigUtils
{
    public static EntityTypeBuilder<Customization> SetPrimaryKey(this EntityTypeBuilder<Customization> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<Customization> SetStronglyTypedIds(this EntityTypeBuilder<Customization> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                x => x.Value,
                v => CustomizationId.New(v)
            );

        builder.Property(x => x.MaterialId)
            .HasConversion(
                x => x.Value,
                v => MaterialId.New(v)
            );

        return builder;
    }

    public static EntityTypeBuilder<Customization> SetValidations(this EntityTypeBuilder<Customization> builder)
    {
        builder.Property(x => x.Scale)
            .IsRequired()
            .HasPrecision(4, 2)
            .HasColumnName("Scale");

        builder.Property(x => x.Infill)
            .IsRequired()
            .HasPrecision(4, 2)
            .HasColumnName("Infill");

        builder.Property(x => x.Volume)
            .IsRequired()
            .HasPrecision(18, 2)
            .HasColumnName("Volume");

        builder.Property(x => x.Color)
            .IsRequired()
            .HasMaxLength(7)
            .HasColumnName("Color");

        builder.Property(x => x.MaterialId)
            .IsRequired()
            .HasColumnName("MaterialId");

        return builder;
    }
}
