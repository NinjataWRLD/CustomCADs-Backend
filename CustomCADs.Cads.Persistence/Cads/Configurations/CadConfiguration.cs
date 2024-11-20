using CustomCADs.Cads.Domain.Cads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Cads.Persistence.Cads.Configurations;

public class CadConfiguration : IEntityTypeConfiguration<Cad>
{
    public void Configure(EntityTypeBuilder<Cad> builder)
        => builder
            .SetPrimaryKey()
            .SetStronglyTypedIds()
            .SetValueObjects()
            .SetValidaitons();
}

public static class CadConfigUtils
{
    public static EntityTypeBuilder<Cad> SetPrimaryKey(this EntityTypeBuilder<Cad> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<Cad> SetStronglyTypedIds(this EntityTypeBuilder<Cad> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                x => x.Value,
                v => new(v)
            );

        return builder;
    }

    public static EntityTypeBuilder<Cad> SetValueObjects(this EntityTypeBuilder<Cad> builder)
    {
        builder.ComplexProperty(x => x.CamCoordinates, a =>
        {
            a.Property(x => x.X).IsRequired().HasColumnName("CamX");
            a.Property(x => x.Y).IsRequired().HasColumnName("CamY");
            a.Property(x => x.Z).IsRequired().HasColumnName("CamZ");
        });

        builder.ComplexProperty(x => x.PanCoordinates, a =>
        {
            a.Property(x => x.X).IsRequired().HasColumnName("PanX");
            a.Property(x => x.Y).IsRequired().HasColumnName("PanY");
            a.Property(x => x.Z).IsRequired().HasColumnName("PanZ");
        });

        return builder;
    }

    public static EntityTypeBuilder<Cad> SetValidaitons(this EntityTypeBuilder<Cad> builder)
    {
        builder.Property(x => x.Key)
            .IsRequired()
            .HasColumnName("Key");

        return builder;
    }
}
