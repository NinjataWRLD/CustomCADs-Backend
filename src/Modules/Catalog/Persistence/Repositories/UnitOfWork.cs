using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Shared.Core.Common.Exceptions.Persistence;

namespace CustomCADs.Catalog.Persistence.Repositories;

public class UnitOfWork(CatalogContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        try
        {
            await context.SaveChangesAsync(ct).ConfigureAwait(false);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw DatabaseConflictException.Custom(ex.Message);
        }
        catch (DbUpdateException ex)
        {
            throw DatabaseException.Custom(ex.Message);
        }
    }

    public async Task ClearProductTagsAsync(ProductId[] ids, string tag, CancellationToken ct = default)
        => await context.ProductTags
            .Include(x => x.Tag)
            .Where(x => ids.Contains(x.ProductId))
            .Where(x => x.Tag.Name == tag)
            .ExecuteDeleteAsync(ct)
            .ConfigureAwait(false);
}
