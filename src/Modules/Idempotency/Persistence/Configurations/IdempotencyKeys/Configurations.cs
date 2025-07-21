using CustomCADs.Idempotency.Domain.IdempotencyKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Idempotency.Persistence.Configurations.IdempotencyKeys;

internal class Configurations : IEntityTypeConfiguration<IdempotencyKey>
{
	public void Configure(EntityTypeBuilder<IdempotencyKey> builder)
		=> builder
			.SetPrimaryKey()
			.SetStronglyTypedIds()
			.SetIndexes()
			.SetValidations();
}
