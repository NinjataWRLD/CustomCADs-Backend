namespace CustomCADs.Cart.Domain.ItemDeliveries.Reads;

public interface IItemDeliveryReads
{
    Task<IEnumerable<ItemDelivery>> AllAsync(bool track = true, CancellationToken ct = default);
    Task<ItemDelivery?> SingleByIdAsync(Guid id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(Guid id, CancellationToken ct = default);
    Task<int> CountAsync(CancellationToken ct = default);
}
