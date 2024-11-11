using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Catalog.Domain.Categories.Reads;

public interface ICategoryReads
{
    Task<IEnumerable<Category>> AllAsync(bool track = true, CancellationToken ct = default);
    Task<Category?> SingleByIdAsync(CategoryId id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(CategoryId id, CancellationToken ct = default);
}
