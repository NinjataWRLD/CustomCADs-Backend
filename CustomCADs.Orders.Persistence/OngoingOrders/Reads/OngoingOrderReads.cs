using CustomCADs.Orders.Domain.OngoingOrders;
using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Orders.Domain.OngoingOrders.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Orders.Persistence.OngoingOrders.Reads;

public sealed class OngoingOrderReads(OrdersContext context) : IOngoingOrderReads
{
    public async Task<Result<OngoingOrder>> AllAsync(OngoingOrderQuery query, bool track = true, CancellationToken ct = default)
    {
        IQueryable<OngoingOrder> queryable = context.OngoingOrders
            .WithTracking(track)
            .WithFilter(query.Delivery, query.OrderStatus, query.BuyerId, query.DesignerId)
            .WithSearch(query.Name);

        int count = await queryable.CountAsync(ct).ConfigureAwait(false);
        OngoingOrder[] orders = await queryable
            .WithSorting(query.Sorting ?? new())
            .WithPagination(query.Pagination.Page, query.Pagination.Limit)
            .ToArrayAsync(ct)
            .ConfigureAwait(false);

        return new(count, orders);
    }

    public async Task<OngoingOrder?> SingleByIdAsync(OngoingOrderId id, bool track = true, CancellationToken ct = default)
        => await context.OngoingOrders
            .WithTracking(track)
            .FirstOrDefaultAsync(o => o.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<bool> ExistsByIdAsync(OngoingOrderId id, CancellationToken ct = default)
        => await context.OngoingOrders
            .WithTracking(false)
            .AnyAsync(o => o.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<Dictionary<OngoingOrderStatus, int>> CountByStatusAsync(AccountId buyerId, CancellationToken ct = default)
        => await context.OngoingOrders
            .WithTracking(false)
            .Where(o => o.BuyerId == buyerId)
            .GroupBy(o => o.OrderStatus)
            .ToDictionaryAsync(x => x.Key, x => x.Count(), ct)
            .ConfigureAwait(false);
}
