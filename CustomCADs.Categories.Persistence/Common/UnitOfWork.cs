using CustomCADs.Categories.Domain.Common;

namespace CustomCADs.Categories.Persistence.Common;

public class UnitOfWork(CategoriesContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct).ConfigureAwait(false);
}
