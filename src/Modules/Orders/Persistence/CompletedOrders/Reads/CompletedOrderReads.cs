using CustomCADs.Orders.Domain.CompletedOrders;
using CustomCADs.Orders.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Orders.Persistence.CompletedOrders.Reads;

public sealed class CompletedOrderReads(OrdersContext context) : ICompletedOrderReads
{
    public async Task<Result<CompletedOrder>> AllAsync(CompletedOrderQuery query, bool track = true, CancellationToken ct = default)
    {
        IQueryable<CompletedOrder> queryable = context.CompletedOrders
            .WithTracking(track)
            .WithFilter(query.Delivery, query.BuyerId, query.DesignerId)
            .WithSearch(query.Name);

        int count = await queryable.CountAsync(ct).ConfigureAwait(false);
        CompletedOrder[] orders = await queryable
            .WithSorting(query.Sorting ?? new())
            .WithPagination(query.Pagination.Page, query.Pagination.Limit)
            .ToArrayAsync(ct)
            .ConfigureAwait(false);

        return new(count, orders);
    }

    public async Task<CompletedOrder?> SingleByIdAsync(CompletedOrderId id, bool track = true, CancellationToken ct = default)
        => await context.CompletedOrders
            .WithTracking(track)
            .FirstOrDefaultAsync(o => o.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<bool> ExistsByIdAsync(CompletedOrderId id, CancellationToken ct = default)
        => await context.CompletedOrders
            .WithTracking(false)
            .AnyAsync(o => o.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<int> CountByBuyerIdAsync(AccountId buyerId, CancellationToken ct = default)
        => await context.CompletedOrders
            .WithTracking(false)
            .Where(o => o.BuyerId == buyerId)
            .CountAsync(ct)
            .ConfigureAwait(false);
}
