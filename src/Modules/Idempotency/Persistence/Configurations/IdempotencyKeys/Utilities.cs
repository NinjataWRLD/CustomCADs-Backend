using CustomCADs.Idempotency.Domain.IdempotencyKeys;
using CustomCADs.Shared.Core.Common.TypedIds.Idempotency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Idempotency.Persistence.Configurations.IdempotencyKeys;

internal static class Utilities
{
	internal static EntityTypeBuilder<IdempotencyKey> SetPrimaryKey(this EntityTypeBuilder<IdempotencyKey> builder)
	{
		builder.HasKey(x => new { x.Id, x.RequestHash });

		return builder;
	}

	internal static EntityTypeBuilder<IdempotencyKey> SetStronglyTypedIds(this EntityTypeBuilder<IdempotencyKey> builder)
	{
		builder.Property(x => x.Id)
			.HasConversion(
				x => x.Value,
				v => IdempotencyKeyId.New(v)
			);

		return builder;
	}

	internal static EntityTypeBuilder<IdempotencyKey> SetIndexes(this EntityTypeBuilder<IdempotencyKey> builder)
	{
		builder.HasIndex(x => x.CreatedAt);

		return builder;
	}

	internal static EntityTypeBuilder<IdempotencyKey> SetValidations(this EntityTypeBuilder<IdempotencyKey> builder)
	{
		builder.Property(x => x.RequestHash)
			.IsRequired()
			.HasColumnName(nameof(IdempotencyKey.RequestHash));

		builder.Property(x => x.ResponseBody)
			.HasColumnName(nameof(IdempotencyKey.ResponseBody));

		builder.Property(x => x.StatusCode)
			.HasColumnName(nameof(IdempotencyKey.StatusCode));

		builder.Property(x => x.CreatedAt)
			.IsRequired()
			.HasColumnName(nameof(IdempotencyKey.CreatedAt));

		return builder;
	}
}
