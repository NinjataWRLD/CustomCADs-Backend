using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Catalog.Persistence.Configurations;

using static ProductConstants;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .SetPrimaryKey()
            .SetForeignKeys()
            .SetValueObjects()
            .SetValidations();
    }
}

static class CadConfigUtils
{
    public static EntityTypeBuilder<Product> SetPrimaryKey(this EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<Product> SetForeignKeys(this EntityTypeBuilder<Product> builder)
    {
        builder
            .HasOne(c => c.Category).WithMany()
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        return builder;
    }

    public static EntityTypeBuilder<Product> SetValueObjects(this EntityTypeBuilder<Product> builder)
    {
        builder.ComplexProperty(p => p.Cad, c =>
        {
            c.ComplexProperty(c => c.CamCoordinates);
            c.ComplexProperty(c => c.PanCoordinates);
        });

        return builder;
    }

    public static EntityTypeBuilder<Product> SetValidations(this EntityTypeBuilder<Product> builder)
    {
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength);

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(DescriptionMaxLength);

        builder.Property(c => c.Status)
            .IsRequired()
            .HasConversion(
                e => e.ToString(),
                s => Enum.Parse<ProductStatus>(s)
            );

        builder.Property(c => c.Cost)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(c => c.UploadDate)
            .IsRequired();

        builder.Property(c => c.CategoryId)
            .IsRequired();

        builder.Property(c => c.CreatorId)
            .IsRequired();

        return builder;
    }
}
