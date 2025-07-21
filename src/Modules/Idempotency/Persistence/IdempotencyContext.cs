using CustomCADs.Idempotency.Domain.IdempotencyKeys;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Idempotency.Persistence;

public class IdempotencyContext(DbContextOptions<IdempotencyContext> opt) : DbContext(opt)
{
	public required DbSet<IdempotencyKey> IdempotencyKeys { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.HasDefaultSchema("Idempotency");
		builder.ApplyConfigurationsFromAssembly(IdempotencyPersistenceReference.Assembly);
	}
}
