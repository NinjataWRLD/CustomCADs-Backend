using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Domain.Orders.Reads;

public interface IOrderReads
{
    Task<OrderResult> AllAsync(OrderQuery query, bool track = true, CancellationToken ct = default);
    Task<Order?> SingleByIdAsync(OrderId id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(OrderId id, CancellationToken ct = default);
    Task<int> CountByStatusAsync(UserId buyerId, OrderStatus status, CancellationToken ct = default);
}
