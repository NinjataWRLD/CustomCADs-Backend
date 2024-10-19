namespace CustomCADs.Catalog.Domain.Categories.Reads;

public interface ICategoryReads
{
    Task<IEnumerable<Category>> AllAsync(bool track = true, CancellationToken ct = default);
    Task<Category?> SingleByIdAsync(int id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(int id, CancellationToken ct = default);
}
