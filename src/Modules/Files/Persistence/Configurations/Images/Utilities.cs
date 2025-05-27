using CustomCADs.Files.Domain.Images;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Files.Persistence.Configurations.Images;

using static Constants.Textures;

public static class Utilities
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
			.HasColumnName(nameof(Image.Key));

		builder.Property(x => x.ContentType)
			.IsRequired()
			.HasColumnName(nameof(Image.ContentType));

		return builder;
	}

	public static EntityTypeBuilder<Image> SetSeeding(this EntityTypeBuilder<Image> builder)
	{
		builder.HasData([
			Image.CreateWithId(PLA, "textures/pla.webp", "image/webp"),
			Image.CreateWithId(ABS, "textures/abs.webp", "image/webp"),
			Image.CreateWithId(GlowInDark, "textures/glow-in-dark.webp", "image/webp"),
			Image.CreateWithId(TUF, "textures/tuf.webp", "image/webp"),
			Image.CreateWithId(Wood, "textures/wood.webp", "image/webp"),
		]);

		return builder;
	}
}
