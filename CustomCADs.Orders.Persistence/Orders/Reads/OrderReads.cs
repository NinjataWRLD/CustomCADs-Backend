using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Orders.Persistence.Orders.Reads;

public sealed class OrderReads(OrdersContext context) : IOrderReads
{
    public async Task<Result<Order>> AllAsync(OrderQuery query, bool track = true, CancellationToken ct = default)
    {
        IQueryable<Order> queryable = context.Orders
            .WithTracking(track)
            .WithFilter(query.DeliveryType, query.OrderStatus, query.BuyerId, query.DesignerId)
            .WithSearch(query.Name);

        int count = await queryable.CountAsync(ct).ConfigureAwait(false);
        Order[] orders = await queryable
            .WithSorting(query.Sorting ?? new())
            .WithPagination(query.Page, query.Limit)
            .ToArrayAsync(ct)
            .ConfigureAwait(false);

        return new(count, orders);
    }

    public async Task<Order?> SingleByIdAsync(OrderId id, bool track = true, CancellationToken ct = default)
        => await context.Orders
            .WithTracking(track)
            .FirstOrDefaultAsync(o => o.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<bool> ExistsByIdAsync(OrderId id, CancellationToken ct = default)
        => await context.Orders
            .WithTracking(false)
            .AnyAsync(o => o.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<Dictionary<OrderStatus, int>> CountByStatusAsync(AccountId buyerId, CancellationToken ct = default)
        => await context.Orders
            .WithTracking(false)
            .Where(o => o.BuyerId == buyerId)
            .GroupBy(o => o.OrderStatus)
            .ToDictionaryAsync(x => x.Key, x => x.Count(), ct)
            .ConfigureAwait(false);
}
