namespace CustomCADs.Cart.Domain.Cart.Reads;

public interface ICartReads
{
    Task<IEnumerable<Cart>> AllAsync(bool track = true, CancellationToken ct = default);
    Task<Cart?> SingleByIdAsync(Guid id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(Guid id, CancellationToken ct = default);
    Task<int> CountAsync(CancellationToken ct = default);
}
