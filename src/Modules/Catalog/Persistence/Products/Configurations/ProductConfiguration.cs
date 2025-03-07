﻿using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Persistence.ShadowEntities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Catalog.Persistence.Products.Configurations;

using static ProductConstants;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .SetPrimaryKey()
            .SetNavigations()
            .SetStronglyTypedIds()
            .SetValueObjects()
            .SetValidations();
    }
}

static class ProductConfigUtils
{
    public static EntityTypeBuilder<Product> SetPrimaryKey(this EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<Product> SetNavigations(this EntityTypeBuilder<Product> builder)
    {
        builder.HasMany<ProductTag>()
               .WithOne()
               .HasForeignKey(pt => pt.ProductId);

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
                .HasColumnName("Purchases");

            c.Property(x => x.Views)
                .IsRequired()
                .HasColumnName("Views");
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
            .HasPrecision(19, 2)
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

        builder.Property(x => x.ImageId)
            .IsRequired()
            .HasColumnName("ImageId");

        builder.Property(x => x.CadId)
            .IsRequired()
            .HasColumnName("CadId");

        builder.Property(x => x.CreatorId)
            .IsRequired()
            .HasColumnName("CreatorId");

        builder.Property(x => x.DesignerId)
            .HasColumnName("DesignerId");

        return builder;
    }
}
