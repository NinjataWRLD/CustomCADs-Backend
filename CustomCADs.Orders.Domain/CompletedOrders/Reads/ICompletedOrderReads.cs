using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Domain.CompletedOrders.Reads;

public interface ICompletedOrderReads
{
    Task<Result<CompletedOrder>> AllAsync(CompletedOrderQuery query, bool track = true, CancellationToken ct = default);
    Task<CompletedOrder?> SingleByIdAsync(CompletedOrderId id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(CompletedOrderId id, CancellationToken ct = default);
    Task<int> CountByBuyerIdAsync(AccountId buyerId, CancellationToken ct = default);
}
