using CustomCADs.Inventory.Domain.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Inventory.Persistence.Products.Configurations;

using static ProductConstants;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .SetPrimaryKey()
            .SetStronglyTypedIds()
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

    public static EntityTypeBuilder<Product> SetStronglyTypedIds(this EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                x => x.Value,
                v => new(v)
            );

        builder.Property(x => x.CategoryId)
            .HasConversion(
                x => x.Value,
                v => new(v)
            );

        builder.Property(x => x.CadId)
            .HasConversion(
                x => x.Value,
                v => new(v)
            );

        builder.Property(x => x.CreatorId)
            .HasConversion(
                x => x.Value,
                v => new(v)
            );

        builder.Property(x => x.DesignerId)
            .HasConversion<Guid?>(
                x => x == null ? null : x.Value.Value,
                v => v == null ? null : new(v.Value)
            );

        return builder;
    }

    public static EntityTypeBuilder<Product> SetValueObjects(this EntityTypeBuilder<Product> builder)
    {
        builder.ComplexProperty(x => x.Image, c =>
        {
            c.Property(x => x.Key)
                .IsRequired()
                .HasColumnName("ImageKey");

            c.Property(x => x.ContentType)
                .IsRequired()
                .HasColumnName("ImageContentType");
        });

        return builder;
    }

    public static EntityTypeBuilder<Product> SetValidations(this EntityTypeBuilder<Product> builder)
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
            .HasColumnName("Price");

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion(
                e => e.ToString(),
                s => Enum.Parse<ProductStatus>(s)
            ).HasColumnName("Status");

        builder.Property(x => x.UploadDate)
            .IsRequired()
            .HasColumnName("UploadDate");

        builder.Property(x => x.CategoryId)
            .IsRequired()
            .HasColumnName("CategoryId");

        builder.Property(x => x.CadId)
            .IsRequired()
            .HasColumnName("CadId");

        builder.Property(x => x.CreatorId)
            .IsRequired()
            .HasColumnName("CreatorId");

        builder.Property(x => x.DesignerId)
            .IsRequired()
            .HasColumnName("DesignerId");

        return builder;
    }
}
