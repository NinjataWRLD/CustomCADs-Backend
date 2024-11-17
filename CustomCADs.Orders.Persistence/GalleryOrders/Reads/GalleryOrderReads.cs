using CustomCADs.Orders.Domain.GalleryOrders.Entities;
using CustomCADs.Orders.Domain.GalleryOrders.Reads;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Orders;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Orders.Persistence.GalleryOrders.Reads;

public class GalleryOrderReads(OrdersContext context) : IGalleryOrderReads
{
    public async Task<GalleryOrderResult> AllAsync(GalleryOrderQuery query, bool track = true, CancellationToken ct = default)
    {
        IQueryable<GalleryOrder> queryable = context.GalleryOrders
            .WithTracking(track)
            .WithFilter(query.BuyerId, query.DeliveryType);

        int count = await queryable.CountAsync(ct).ConfigureAwait(false);
        GalleryOrder[] orders = await queryable
            .WithSorting(query.Sorting ?? new())
            .WithPagination(query.Page, query.Limit)
            .ToArrayAsync(ct)
            .ConfigureAwait(false);

        return new(count, orders);
    }

    public async Task<GalleryOrder?> SingleByIdAsync(GalleryOrderId id, bool track = true, CancellationToken ct = default)
        => await context.GalleryOrders
            .WithTracking(track)
            .FirstOrDefaultAsync(p => p.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<bool> ExistsByIdAsync(GalleryOrderId id, CancellationToken ct = default)
        => await context.GalleryOrders
            .WithTracking(false)
            .AnyAsync(o => o.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<int> CountAsync(UserId buyerId, CancellationToken ct = default)
        => await context.GalleryOrders
            .WithTracking(false)
            .Where(o => o.BuyerId == buyerId)
            .CountAsync(ct)
            .ConfigureAwait(false);
}
