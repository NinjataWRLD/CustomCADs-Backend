namespace CustomCADs.Catalog.Domain.Categories.Reads;

public interface ICategoryReads
{
    Task<IEnumerable<Category>> AllAsync(bool asNoTracking = false);
    Task<Category?> SingleByIdAsync(int id, bool asNoTracking = false, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(int id, CancellationToken ct = default);
}
