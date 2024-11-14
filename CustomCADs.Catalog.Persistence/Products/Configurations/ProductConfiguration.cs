using CustomCADs.Catalog.Domain.Categories.Entities;
using CustomCADs.Catalog.Domain.Products.Entities;
using CustomCADs.Catalog.Domain.Products.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Catalog.Persistence.Products.Configurations;

using static ProductConstants;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .SetPrimaryKey()
            .SetForeignKeys()
            .SetStronglyTypedIds()
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
            .HasOne<Category>()
            .WithMany()
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);

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

        builder.Property(x => x.CreatorId)
            .HasConversion(
                x => x.Value,
                v => new(v)
            );

        builder.Property(x => x.CategoryId)
            .HasConversion(
                x => x.Value,
                v => new(v)
            );

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

        builder.Property(x => x.CreatorId)
            .IsRequired()
            .HasColumnName("CreatorId");

        return builder;
    }
}
