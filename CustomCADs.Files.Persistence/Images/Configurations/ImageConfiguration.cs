using CustomCADs.Files.Domain.Images;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Files.Persistence.Images.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
        => builder
            .SetPrimaryKey()
            .SetStronglyTypedIds()
            .SetValidaitons();
}

public static class ImageConfigUtils
{
    public static EntityTypeBuilder<Image> SetPrimaryKey(this EntityTypeBuilder<Image> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<Image> SetStronglyTypedIds(this EntityTypeBuilder<Image> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                x => x.Value,
                v => ImageId.New(v)
            );

        return builder;
    }

    public static EntityTypeBuilder<Image> SetValidaitons(this EntityTypeBuilder<Image> builder)
    {
        builder.Property(x => x.Key)
            .IsRequired()
            .HasColumnName("Key");

        builder.Property(x => x.ContentType)
            .IsRequired()
            .HasColumnName("ContentType");

        return builder;
    }
}
