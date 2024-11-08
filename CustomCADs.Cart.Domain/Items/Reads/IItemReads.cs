namespace CustomCADs.Cart.Domain.Items.Reads;

public interface IItemReads
{
    Task<IEnumerable<Item>> AllAsync(bool track = true, CancellationToken ct = default);
    Task<Item?> SingleByIdAsync(Guid id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(Guid id, CancellationToken ct = default);
    Task<int> CountAsync(CancellationToken ct = default);
}
