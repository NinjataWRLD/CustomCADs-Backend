using CustomCADs.Shared.Core.Common.TypedIds.Idempotency;

namespace CustomCADs.UnitTests.Idempotency.Domain.IdempotencyKeys.Create.Data;

using static IdempotencyKeysData;

public class IdempotencyKeyCreateInvalidData : TheoryData<IdempotencyKeyId, string>
{
	public IdempotencyKeyCreateInvalidData()
	{
		// Id
		Add(InvalidId, ValidRequestHash);

		// RequestHash
		Add(ValidId, InvalidRequestHash);
	}
}
