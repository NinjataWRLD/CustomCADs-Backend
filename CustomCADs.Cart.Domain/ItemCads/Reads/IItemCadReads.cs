namespace CustomCADs.Cart.Domain.ItemCads.Reads;

public interface IItemCadReads
{
    Task<IEnumerable<ItemCad>> AllAsync(bool track = true, CancellationToken ct = default);
    Task<ItemCad?> SingleByIdAsync(Guid id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(Guid id, CancellationToken ct = default);
    Task<int> CountAsync(CancellationToken ct = default);
}
