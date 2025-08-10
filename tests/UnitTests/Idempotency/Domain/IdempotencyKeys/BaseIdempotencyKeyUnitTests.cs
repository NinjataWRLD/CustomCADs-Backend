using CustomCADs.Shared.Domain.TypedIds.Idempotency;

namespace CustomCADs.UnitTests.Idempotency.Domain.IdempotencyKeys;

using static IdempotencyKeysData;

public class BaseIdempotencyKeyUnitTests
{
	protected static IdempotencyKey CreateIdempotencyKey(
		IdempotencyKeyId id,
		string hash
	) => IdempotencyKey.Create(
			id: id,
			hash: hash
		);

	protected static IdempotencyKey CreateIdempotencyKey()
		=> IdempotencyKey.Create(
			id: ValidId,
			hash: ValidRequestHash
		);
}
