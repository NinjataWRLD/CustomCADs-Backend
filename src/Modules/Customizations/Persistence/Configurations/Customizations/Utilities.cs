﻿using CustomCADs.Customizations.Domain.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Customizations.Persistence.Configurations.Customizations;

public static class Utilities
{
	public static EntityTypeBuilder<Customization> SetPrimaryKey(this EntityTypeBuilder<Customization> builder)
	{
		builder.HasKey(x => x.Id);

		return builder;
	}

	public static EntityTypeBuilder<Customization> SetStronglyTypedIds(this EntityTypeBuilder<Customization> builder)
	{
		builder.Property(x => x.Id)
			.ValueGeneratedOnAdd()
			.HasConversion(
				x => x.Value,
				v => CustomizationId.New(v)
			);

		builder.Property(x => x.MaterialId)
			.HasConversion(
				x => x.Value,
				v => MaterialId.New(v)
			);

		return builder;
	}

	public static EntityTypeBuilder<Customization> SetValidations(this EntityTypeBuilder<Customization> builder)
	{
		builder.Property(x => x.Scale)
			.IsRequired()
			.HasPrecision(4, 2)
			.HasColumnName(nameof(Customization.Scale));

		builder.Property(x => x.Infill)
			.IsRequired()
			.HasPrecision(4, 2)
			.HasColumnName(nameof(Customization.Infill));

		builder.Property(x => x.Volume)
			.IsRequired()
			.HasPrecision(18, 2)
			.HasColumnName(nameof(Customization.Volume));

		builder.Property(x => x.Color)
			.IsRequired()
			.HasMaxLength(7)
			.HasColumnName(nameof(Customization.Color));

		builder.Property(x => x.MaterialId)
			.IsRequired()
			.HasColumnName(nameof(Customization.MaterialId));

		return builder;
	}
}
