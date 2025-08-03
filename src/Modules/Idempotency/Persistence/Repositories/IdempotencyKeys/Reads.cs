using CustomCADs.Idempotency.Domain.IdempotencyKeys;
using CustomCADs.Idempotency.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Idempotency;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Idempotency.Persistence.Repositories.IdempotencyKeys;

public class Reads(IdempotencyContext context) : IIdempotencyKeyReads
{
	public async Task<IdempotencyKey?> SingleByIdAsync(IdempotencyKeyId id, string requestHash, bool track = true, CancellationToken ct = default)
		=> await context.IdempotencyKeys
			.WithTracking(track)
			.FirstOrDefaultAsync(x => x.Id == id && x.RequestHash == requestHash, ct)
			.ConfigureAwait(false);
}
