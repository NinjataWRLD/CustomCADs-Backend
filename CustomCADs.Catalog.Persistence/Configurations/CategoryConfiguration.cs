using CustomCADs.Catalog.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static CustomCADs.Catalog.Domain.Categories.CategoryConstants;

namespace CustomCADs.Catalog.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .SetTable()
            .SetPrimaryKey()
            .SetValidations()
            .SetSeedData();
    }
}

static class CategoryConfigUtils
{
    public static EntityTypeBuilder<Category> SetTable(this EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories", schema: "Catalog");

        return builder;
    }

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
        string[] categoriesNames =
        [
            "Animals",
            "Characters",
            "Electronics",
            "Fashion",
            "Furniture",
            "Nature",
            "Science",
            "Sports",
            "Toys",
            "Vehicles",
            "Others",
        ];

        int index = 0;
        builder.HasData(categoriesNames.Select(cn => new Category() { Id = ++index, Name = cn }));

        return builder;
    }
}
