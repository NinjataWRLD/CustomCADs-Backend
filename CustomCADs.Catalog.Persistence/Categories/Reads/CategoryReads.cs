using CustomCADs.Catalog.Domain.Categories.Entities;
using CustomCADs.Catalog.Domain.Categories.Reads;
using CustomCADs.Shared.Persistence;

namespace CustomCADs.Catalog.Persistence.Categories.Reads;

public class CategoryReads(CatalogContext context) : ICategoryReads
{
    public async Task<IEnumerable<Category>> AllAsync(bool track = true, CancellationToken ct = default)
        => await context.Categories
            .WithTracking(track)
            .ToArrayAsync(ct)
            .ConfigureAwait(false);

    public async Task<Category?> SingleByIdAsync(CategoryId id, bool track = true, CancellationToken ct = default)
        => await context.Categories
            .WithTracking(track)
            .FirstOrDefaultAsync(c => c.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<bool> ExistsByIdAsync(CategoryId id, CancellationToken ct = default)
        => await context.Categories
            .WithTracking(false)
            .AnyAsync(c => c.Id == id, ct)
            .ConfigureAwait(false);
}
