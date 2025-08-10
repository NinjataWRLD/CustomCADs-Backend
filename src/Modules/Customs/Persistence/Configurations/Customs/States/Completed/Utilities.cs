using CustomCADs.Customs.Domain.Customs;
using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Customs.States.Entities;
using CustomCADs.Shared.Domain.TypedIds.Customs;
using CustomCADs.Shared.Domain.TypedIds.Delivery;
using CustomCADs.Shared.Domain.TypedIds.Printing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Customs.Persistence.Configurations.Customs.States.Completed;

public static class Utilities
{
	public static EntityTypeBuilder<CompletedCustom> SetPrimaryKey(this EntityTypeBuilder<CompletedCustom> builder)
	{
		builder.HasKey(x => x.CustomId);

		return builder;
	}

	public static EntityTypeBuilder<CompletedCustom> SetStronglyTypedIds(this EntityTypeBuilder<CompletedCustom> builder)
	{
		builder.Property(x => x.CustomId)
			.HasConversion(
				x => x.Value,
				v => CustomId.New(v)
			);

		builder.Property(x => x.ShipmentId)
			.HasConversion(
				x => ShipmentId.Unwrap(x),
				v => ShipmentId.New(v)
			);

		builder.Property(x => x.CustomizationId)
			.HasConversion(
				x => CustomizationId.Unwrap(x),
				v => CustomizationId.New(v)
			);

		return builder;
	}

	public static EntityTypeBuilder<CompletedCustom> SetNavigations(this EntityTypeBuilder<CompletedCustom> builder)
	{
		builder
			.HasOne<Custom>()
			.WithOne(x => x.CompletedCustom)
			.HasForeignKey<CompletedCustom>(x => x.CustomId);

		return builder;
	}

	public static EntityTypeBuilder<CompletedCustom> SetValidations(this EntityTypeBuilder<CompletedCustom> builder)
	{
		builder.Property(x => x.PaymentStatus)
			.IsRequired()
			.HasConversion(
				v => v.ToString(),
				s => Enum.Parse<PaymentStatus>(s)
			).HasColumnName(nameof(CompletedCustom.PaymentStatus));

		builder.Property(x => x.ShipmentId)
			.HasColumnName(nameof(CompletedCustom.ShipmentId));

		builder.Property(x => x.CustomizationId)
			.HasColumnName(nameof(CompletedCustom.CustomizationId));

		return builder;
	}
}
