using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Catalog.Persistence.Configurations.Products;

using static ProductConstants;

static class Utilities
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
                v => ProductId.New(v)
            );

        builder.Property(x => x.CategoryId)
            .HasConversion(
                x => x.Value,
                v => CategoryId.New(v)
            );

        builder.Property(x => x.ImageId)
            .HasConversion(
                x => x.Value,
                v => ImageId.New(v)
            );

        builder.Property(x => x.CadId)
            .HasConversion(
                x => x.Value,
                v => CadId.New(v)
            );

        builder.Property(x => x.CreatorId)
            .HasConversion(
                x => x.Value,
                v => AccountId.New(v)
            );

        builder.Property(x => x.DesignerId)
            .HasConversion<Guid?>(
                x => x == null ? null : x.Value.Value,
                v => AccountId.New(v)
            );

        return builder;
    }

    public static EntityTypeBuilder<Product> SetValueObjects(this EntityTypeBuilder<Product> builder)
    {
        builder.ComplexProperty(x => x.Counts, c =>
        {
            c.Property(x => x.Purchases)
                .IsRequired()
                .HasColumnName(nameof(Product.Counts.Purchases));

            c.Property(x => x.Views)
                .IsRequired()
                .HasColumnName(nameof(Product.Counts.Views));
        });

        return builder;
    }

    public static EntityTypeBuilder<Product> SetValidations(this EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength)
            .HasColumnName(nameof(Product.Name));

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(DescriptionMaxLength)
            .HasColumnName(nameof(Product.Description));

        builder.Property(x => x.Price)
            .IsRequired()
            .HasPrecision(19, 2)
            .HasColumnName(nameof(Product.Price));

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion(
                e => e.ToString(),
                s => Enum.Parse<ProductStatus>(s)
            ).HasColumnName(nameof(Product.Status));

        builder.Property(x => x.UploadedAt)
            .IsRequired()
            .HasColumnName(nameof(Product.UploadedAt));

        builder.Property(x => x.CategoryId)
            .IsRequired()
            .HasColumnName(nameof(Product.CategoryId));

        builder.Property(x => x.ImageId)
            .IsRequired()
            .HasColumnName(nameof(Product.ImageId));

        builder.Property(x => x.CadId)
            .IsRequired()
            .HasColumnName(nameof(Product.CadId));

        builder.Property(x => x.CreatorId)
            .IsRequired()
            .HasColumnName(nameof(Product.CreatorId));

        builder.Property(x => x.DesignerId)
            .HasColumnName(nameof(Product.DesignerId));

        return builder;
    }
}
