using CustomCADs.Customs.Domain.Customs;
using CustomCADs.Customs.Domain.Customs.States.Entities;
using CustomCADs.Shared.Domain.TypedIds.Customs;
using CustomCADs.Shared.Domain.TypedIds.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Customs.Persistence.Configurations.Customs.States.Finished;

public static class Utilities
{
	public static EntityTypeBuilder<FinishedCustom> SetPrimaryKey(this EntityTypeBuilder<FinishedCustom> builder)
	{
		builder.HasKey(x => x.CustomId);

		return builder;
	}

	public static EntityTypeBuilder<FinishedCustom> SetStronglyTypedIds(this EntityTypeBuilder<FinishedCustom> builder)
	{
		builder.Property(x => x.CustomId)
			.HasConversion(
				x => x.Value,
				v => CustomId.New(v)
			);

		builder.Property(x => x.CadId)
			.HasConversion(
				x => x.Value,
				v => CadId.New(v)
			);

		return builder;
	}

	public static EntityTypeBuilder<FinishedCustom> SetNavigations(this EntityTypeBuilder<FinishedCustom> builder)
	{
		builder
			.HasOne<Custom>()
			.WithOne(x => x.FinishedCustom)
			.HasForeignKey<FinishedCustom>(x => x.CustomId);

		return builder;
	}

	public static EntityTypeBuilder<FinishedCustom> SetValidations(this EntityTypeBuilder<FinishedCustom> builder)
	{
		builder.Property(x => x.Price)
			.IsRequired()
			.HasPrecision(19, 2)
			.HasColumnName(nameof(FinishedCustom.Price));

		builder.Property(x => x.FinishedAt)
			.IsRequired()
			.HasColumnName(nameof(FinishedCustom.FinishedAt));

		builder.Property(x => x.CadId)
			.IsRequired()
			.HasColumnName(nameof(FinishedCustom.CadId));

		return builder;
	}
}
