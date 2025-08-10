using CustomCADs.Files.Domain.Cads;
using CustomCADs.Shared.Domain.TypedIds.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Files.Persistence.Configurations.Cads;

public static class Utilities
{
	public static EntityTypeBuilder<Cad> SetPrimaryKey(this EntityTypeBuilder<Cad> builder)
	{
		builder.HasKey(x => x.Id);

		return builder;
	}

	public static EntityTypeBuilder<Cad> SetStronglyTypedIds(this EntityTypeBuilder<Cad> builder)
	{
		builder.Property(x => x.Id)
			.ValueGeneratedOnAdd()
			.HasConversion(
				x => x.Value,
				v => CadId.New(v)
			);

		return builder;
	}

	public static EntityTypeBuilder<Cad> SetValueObjects(this EntityTypeBuilder<Cad> builder)
	{
		builder.ComplexProperty(x => x.CamCoordinates, a =>
		{
			a.Property(x => x.X).IsRequired().HasColumnName("CamX");
			a.Property(x => x.Y).IsRequired().HasColumnName("CamY");
			a.Property(x => x.Z).IsRequired().HasColumnName("CamZ");
		});

		builder.ComplexProperty(x => x.PanCoordinates, a =>
		{
			a.Property(x => x.X).IsRequired().HasColumnName("PanX");
			a.Property(x => x.Y).IsRequired().HasColumnName("PanY");
			a.Property(x => x.Z).IsRequired().HasColumnName("PanZ");
		});

		return builder;
	}

	public static EntityTypeBuilder<Cad> SetValidaitons(this EntityTypeBuilder<Cad> builder)
	{
		builder.Property(x => x.Key)
			.IsRequired()
			.HasColumnName(nameof(Cad.Key));

		builder.Property(x => x.ContentType)
			.IsRequired()
			.HasColumnName(nameof(Cad.ContentType));

		builder.Property(x => x.Volume)
			.IsRequired()
			.HasColumnName(nameof(Cad.Volume));

		return builder;
	}
}
