using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Catalog.Persistence.Configurations;

using static CategoryConstants;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .SetPrimaryKey()
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

    public static EntityTypeBuilder<Category> SetValidations(this EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength);

        return builder;
    }

    public static EntityTypeBuilder<Category> SetSeedData(this EntityTypeBuilder<Category> builder)
    {
        builder.HasData(Category.CreateRange([
            (1, "Animals"),
            (2, "Characters"),
            (3, "Electronics"),
            (4, "Fashion"),
            (5, "Furniture"),
            (6, "Nature"),
            (7, "Science"),
            (8, "Sports"),
            (9, "Toys"),
            (10, "Vehicles"),
            (11, "Others"),
        ]));

        return builder;
    }
}
