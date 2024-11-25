using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Orders.Persistence.Orders.Reads;

public class OrderReads(OrdersContext context) : IOrderReads
{
    public async Task<OrderResult> AllAsync(OrderQuery query, bool track = true, CancellationToken ct = default)
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

    public async Task<int> CountByStatusAsync(UserId buyerId, OrderStatus status, CancellationToken ct = default)
        => await context.Orders
            .WithTracking(false)
            .Where(o => o.BuyerId == buyerId)
            .CountAsync(o => o.OrderStatus == status, ct)
            .ConfigureAwait(false);
}
