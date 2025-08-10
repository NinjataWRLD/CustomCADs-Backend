using CustomCADs.Customs.Domain.Customs;
using CustomCADs.Customs.Domain.Customs.States.Entities;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Customs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Customs.Persistence.Configurations.Customs.States.Accepted;

public static class Utilities
{
	public static EntityTypeBuilder<AcceptedCustom> SetPrimaryKey(this EntityTypeBuilder<AcceptedCustom> builder)
	{
		builder.HasKey(x => x.CustomId);

		return builder;
	}

	public static EntityTypeBuilder<AcceptedCustom> SetStronglyTypedIds(this EntityTypeBuilder<AcceptedCustom> builder)
	{
		builder.Property(x => x.CustomId)
			.HasConversion(
				x => x.Value,
				v => CustomId.New(v)
			);

		builder.Property(x => x.DesignerId)
			.HasConversion(
				x => x.Value,
				v => AccountId.New(v)
			);

		return builder;
	}

	public static EntityTypeBuilder<AcceptedCustom> SetNavigations(this EntityTypeBuilder<AcceptedCustom> builder)
	{
		builder
			.HasOne<Custom>()
			.WithOne(x => x.AcceptedCustom)
			.HasForeignKey<AcceptedCustom>(x => x.CustomId);

		return builder;
	}

	public static EntityTypeBuilder<AcceptedCustom> SetValidations(this EntityTypeBuilder<AcceptedCustom> builder)
	{
		builder.Property(x => x.AcceptedAt)
			.IsRequired()
			.HasColumnName(nameof(AcceptedCustom.AcceptedAt));

		builder.Property(x => x.DesignerId)
			.IsRequired()
			.HasColumnName(nameof(AcceptedCustom.DesignerId));

		return builder;
	}
}
