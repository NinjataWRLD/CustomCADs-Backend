using CustomCADs.Orders.Domain.CustomOrders.Entities;
using CustomCADs.Orders.Domain.CustomOrders.Enums;
using CustomCADs.Orders.Domain.CustomOrders.Reads;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Orders;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Orders.Persistence.CustomOrders.Reads;

public class CustomOrderReads(OrdersContext context) : ICustomOrderReads
{
    public async Task<CustomOrderResult> AllAsync(CustomOrderQuery query, bool track = true, CancellationToken ct = default)
    {
        IQueryable<CustomOrder> queryable = context.CustomOrders
            .WithTracking(track)
            .WithFilter(query.DeliveryType, query.OrderStatus, query.BuyerId, query.DesignerId)
            .WithSearch(query.Name);

        int count = await queryable.CountAsync(ct).ConfigureAwait(false);
        CustomOrder[] orders = await queryable
            .WithSorting(query.Sorting ?? new())
            .WithPagination(query.Page, query.Limit)
            .ToArrayAsync(ct)
            .ConfigureAwait(false);

        return new(count, orders);
    }

    public async Task<CustomOrder?> SingleByIdAsync(CustomOrderId id, bool track = true, CancellationToken ct = default)
        => await context.CustomOrders
            .WithTracking(track)
            .FirstOrDefaultAsync(o => o.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<bool> ExistsByIdAsync(CustomOrderId id, CancellationToken ct = default)
        => await context.CustomOrders
            .WithTracking(false)
            .AnyAsync(o => o.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<int> CountByStatusAsync(UserId buyerId, CustomOrderStatus status, CancellationToken ct = default)
        => await context.CustomOrders
            .WithTracking(false)
            .Where(o => o.BuyerId == buyerId)
            .CountAsync(o => o.OrderStatus == status, ct)
            .ConfigureAwait(false);
}
