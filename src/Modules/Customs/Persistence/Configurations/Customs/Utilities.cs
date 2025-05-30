using CustomCADs.Customs.Domain.Customs;
using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Customs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Customs.Persistence.Configurations.Customs;

using static CustomConstants;

public static class Utilities
{
	public static EntityTypeBuilder<Custom> SetPrimaryKey(this EntityTypeBuilder<Custom> builder)
	{
		builder.HasKey(x => x.Id);

		return builder;
	}

	public static EntityTypeBuilder<Custom> SetStronglyTypedIds(this EntityTypeBuilder<Custom> builder)
	{
		builder.Property(x => x.Id)
			.ValueGeneratedOnAdd()
			.HasConversion(
				x => x.Value,
				v => CustomId.New(v)
			);

		builder.Property(x => x.BuyerId)
			.HasConversion(
				x => x.Value,
				v => AccountId.New(v)
			);

		return builder;
	}

	public static EntityTypeBuilder<Custom> SetValidations(this EntityTypeBuilder<Custom> builder)
	{
		builder.Property(x => x.Name)
			.IsRequired()
			.HasMaxLength(NameMaxLength)
			.HasColumnName(nameof(Custom.Name));

		builder.Property(x => x.Description)
			.IsRequired()
			.HasMaxLength(DescriptionMaxLength)
			.HasColumnName(nameof(Custom.Description));

		builder.Property(x => x.ForDelivery)
			.IsRequired()
			.HasColumnName(nameof(Custom.ForDelivery));

		builder.Property(x => x.OrderedAt)
			.IsRequired()
			.HasColumnName(nameof(Custom.OrderedAt));

		builder.Property(x => x.CustomStatus)
			.IsRequired()
			.HasConversion(
				x => x.ToString(),
				s => Enum.Parse<CustomStatus>(s)
			).HasColumnName(nameof(Custom.CustomStatus));

		builder.Property(x => x.BuyerId)
			.IsRequired()
			.HasColumnName(nameof(Custom.BuyerId));

		return builder;
	}
}
