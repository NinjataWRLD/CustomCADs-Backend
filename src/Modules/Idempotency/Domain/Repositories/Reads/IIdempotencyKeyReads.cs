using CustomCADs.Idempotency.Domain.IdempotencyKeys;

namespace CustomCADs.Idempotency.Domain.Repositories.Reads;

public interface IIdempotencyKeyReads
{
	Task<IdempotencyKey?> SingleByIdAsync(IdempotencyKeyId id, string requestHash, bool track = true, CancellationToken ct = default);
}
