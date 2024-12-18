﻿using CustomCADs.Categories.Domain.Categories;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Categories.Persistence.Categories.Configurations;

using static CategoryConstants;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .SetPrimaryKey()
            .SetStronglyTypedIds()
            .SetValidations()
            .SetSeedData();
    }
}

static class CategoryConfigUtils
{
    public static EntityTypeBuilder<Category> SetPrimaryKey(this EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<Category> SetStronglyTypedIds(this EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Id)
            .HasConversion(
                x => x.Value,
                v => new(v)
            ).UseIdentityColumn();

        return builder;
    }

    public static EntityTypeBuilder<Category> SetValidations(this EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength)
            .HasColumnName("Name");

        return builder;
    }

    public static EntityTypeBuilder<Category> SetSeedData(this EntityTypeBuilder<Category> builder)
    {
        builder.HasData(Category.CreateRange([
            (new(1), "Animals"),
            (new(2), "Characters"),
            (new(3), "Electronics"),
            (new(4), "Fashion"),
            (new(5), "Furniture"),
            (new(6), "Nature"),
            (new(7), "Science"),
            (new(8), "Sports"),
            (new(9), "Toys"),
            (new(10), "Vehicles"),
            (new(11), "Others"),
        ]));

        return builder;
    }
}
