using CustomCADs.Catalog.Domain.Tags;
using CustomCADs.Catalog.Persistence.ShadowEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Catalog.Persistence.Configurations.Tags;

using static TagConstants;

static class Utilities
{
    public static EntityTypeBuilder<Tag> SetPrimaryKey(this EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<Tag> SetNavigations(this EntityTypeBuilder<Tag> builder)
    {
        builder.HasMany<ProductTag>()
               .WithOne()
               .HasForeignKey(pt => pt.TagId);

        return builder;
    }

    public static EntityTypeBuilder<Tag> SetStronglyTypedIds(this EntityTypeBuilder<Tag> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                x => x.Value,
                v => TagId.New(v)
            );

        return builder;
    }

    public static EntityTypeBuilder<Tag> SetValidations(this EntityTypeBuilder<Tag> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength)
            .HasColumnName(nameof(Tag.Name));

        return builder;
    }

    public static EntityTypeBuilder<Tag> SetSeeding(this EntityTypeBuilder<Tag> builder)
    {
        builder.HasData([
            Tag.CreateWithId(TagId.New(Guid.Parse("5957f822-77a3-4a72-964d-bf7740e994a5")), "Popular"),
            Tag.CreateWithId(TagId.New(Guid.Parse("e67f88d5-330a-414d-b45d-32c6806725ab")), "Professional"),
            Tag.CreateWithId(TagId.New(Guid.Parse("6813c4b9-bcde-4f95-a1ce-8e545756c8a4")), "New"),
        ]);

        return builder;
    }
}
