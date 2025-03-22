using CustomCADs.Orders.Domain.OngoingOrders;
using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Domain.Repositories.Reads;

public interface IOngoingOrderReads
{
    Task<Result<OngoingOrder>> AllAsync(OngoingOrderQuery query, bool track = true, CancellationToken ct = default);
    Task<OngoingOrder?> SingleByIdAsync(OngoingOrderId id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(OngoingOrderId id, CancellationToken ct = default);
    Task<Dictionary<OngoingOrderStatus, int>> CountByStatusAsync(AccountId buyerId, CancellationToken ct = default);
}
