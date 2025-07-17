namespace CustomCADs.Idempotency.Domain.Repositories;

public interface IUnitOfWork
{
	Task SaveChangesAsync(CancellationToken ct = default);
	Task ClearIdempotencyKeysAsync(DateTimeOffset before, CancellationToken ct = default);
}
