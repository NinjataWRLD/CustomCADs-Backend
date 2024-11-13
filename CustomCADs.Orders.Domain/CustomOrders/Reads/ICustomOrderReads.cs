using CustomCADs.Orders.Domain.CustomOrders.Entities;
using CustomCADs.Orders.Domain.CustomOrders.Enums;

namespace CustomCADs.Orders.Domain.CustomOrders.Reads;

public interface ICustomOrderReads
{
    Task<CustomOrderResult> AllAsync(CustomOrderQuery query, bool track = true, CancellationToken ct = default);
    Task<CustomOrder?> SingleByIdAsync(CustomOrderId id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(CustomOrderId id, CancellationToken ct = default);
    Task<int> CountByStatusAsync(CustomOrderId buyerId, CustomOrderStatus status, CancellationToken ct = default);
}
