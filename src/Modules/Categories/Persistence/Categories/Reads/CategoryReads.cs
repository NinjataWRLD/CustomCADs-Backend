using CustomCADs.Categories.Domain.Categories;
using CustomCADs.Categories.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.Shared.Persistence;

namespace CustomCADs.Categories.Persistence.Categories.Reads;

public sealed class CategoryReads(CategoriesContext context) : ICategoryReads
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
