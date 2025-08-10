using CustomCADs.Printing.Domain.Materials;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.Core.Common.TypedIds.Printing;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Printing.Persistence.Configurations.Materials;

using static Constants.Textures;
using static MaterialConstants;

static class Utilities
{
	public static EntityTypeBuilder<Material> SetPrimaryKey(this EntityTypeBuilder<Material> builder)
	{
		builder.HasKey(x => x.Id);

		return builder;
	}

	public static EntityTypeBuilder<Material> SetStronglyTypedIds(this EntityTypeBuilder<Material> builder)
	{
		builder.Property(x => x.Id)
			.HasConversion(
				x => x.Value,
				v => MaterialId.New(v)
			).UseIdentityColumn();

		builder.Property(x => x.TextureId)
			.HasConversion(
				x => x.Value,
				v => ImageId.New(v)
			);

		return builder;
	}

	public static EntityTypeBuilder<Material> SetValidations(this EntityTypeBuilder<Material> builder)
	{
		builder.Property(x => x.Name)
			.IsRequired()
			.HasMaxLength(NameMaxLength)
			.HasColumnName(nameof(Material.Name));

		builder.Property(x => x.Density)
			.IsRequired()
			.HasPrecision(18, 2)
			.HasColumnName(nameof(Material.Density))
			.HasComment("Measured in g/cm³");

		builder.Property(x => x.Cost)
			.IsRequired()
			.HasPrecision(18, 2)
			.HasColumnName(nameof(Material.Cost))
			.HasComment("Measured in EUR/kg");

		builder.Property(x => x.TextureId)
			.IsRequired()
			.HasColumnName(nameof(Material.TextureId));

		return builder;
	}

	public static EntityTypeBuilder<Material> SetSeeding(this EntityTypeBuilder<Material> builder)
	{
		builder.HasData([
			Material.CreateWithId(MaterialId.New(1), "PLA", 1.24m, 30m, PLA),
			Material.CreateWithId(MaterialId.New(2), "ABS", 1.04m, 30m, ABS),
			Material.CreateWithId(MaterialId.New(3), "Glow in dark", 1.25m, 30m, GlowInDark),
			Material.CreateWithId(MaterialId.New(4), "TUF", 1.27m, 30m, TUF),
			Material.CreateWithId(MaterialId.New(5), "Wood", 1.23m, 30m, Wood),
		]);

		return builder;
	}
}
