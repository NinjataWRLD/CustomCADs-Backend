namespace CustomCADs.Catalog.Domain.Tags.Reads;

public interface ITagReads
{
    Task<Tag[]> AllAsync(bool track = true, CancellationToken ct = default);
    Task<Tag?> SingleByIdAsync(TagId id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(TagId id, CancellationToken ct = default);
    Task<Dictionary<TagId, int>> CountByProductAsync(CancellationToken ct = default);
}
