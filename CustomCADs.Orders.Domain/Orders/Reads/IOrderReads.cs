using CustomCADs.Orders.Domain.Orders.Enums;

namespace CustomCADs.Orders.Domain.Orders.Reads;

public interface IOrderReads
{
    Task<IEnumerable<Order>> AllAsync(bool track = true, CancellationToken ct = default);
    Task<Order?> SingleByIdAsync(Guid id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(Guid id, CancellationToken ct = default);
    Task<int> CountByStatusAsync(Guid buyerId, OrderStatus status, CancellationToken ct = default);
}
