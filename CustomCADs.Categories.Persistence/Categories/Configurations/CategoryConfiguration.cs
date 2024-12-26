using CustomCADs.Categories.Domain.Categories;
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
            
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(DescriptionMaxLength)
            .HasColumnName("Description");

        return builder;
    }

    public static EntityTypeBuilder<Category> SetSeedData(this EntityTypeBuilder<Category> builder)
    {
        builder.HasData(Category.CreateRange([
            (new(1), "Animals", "Includes pets, wild animals, etc."),
            (new(2), "Characters", "Includes movie characters, book characters, game characters, etc."),
            (new(3), "Electronics", "Includes phones, computers, e-devices, earphones, etc."),
            (new(4), "Fashion", "Includes clothes, shoes, accessories, hats, etc."),
            (new(5), "Furniture", "Includes tables, chairs, beds, etc."),
            (new(6), "Nature", "Includes flowers, forests, seas, etc."),
            (new(7), "Science", "Includes organs, tools, chemical fluids, etc."),
            (new(8), "Sports", "Includes footballs, boxing gloves, hockey sticks, etc."),
            (new(9), "Toys", "Includes pet toys, action figures, plushies, etc."),
            (new(10), "Vehicles", "Includes cars, trucks, tanks, bikes, planes, ships, etc."),
            (new(11), "Others", "Includes anything that doesn't fit into the other categories."),
        ]));

        return builder;
    }
}
