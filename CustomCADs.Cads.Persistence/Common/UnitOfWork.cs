using CustomCADs.Cads.Domain.Common;

namespace CustomCADs.Cads.Persistence.Common;

public class UnitOfWork(CadsContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct).ConfigureAwait(false);
}
