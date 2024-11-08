using CustomCADs.Shared.Core.Enums;

namespace CustomCADs.Orders.Domain.OrderDeliveries.Reads;

public interface IOrderDeliveryReads
{
    Task<IEnumerable<OrderDelivery>> AllAsync(bool track = true, CancellationToken ct = default);
    Task<OrderDelivery> SingleByIdAsync(Guid id, bool track = true, CancellationToken ct = default);
    Task<OrderDelivery> ExistsByIdAsync(Guid id, CancellationToken ct = default);
    Task<OrderDelivery> CountByStatusAsync(Guid buyerId, DeliveryStatus status, CancellationToken ct = default);
}
