using CustomCADs.Delivery.Domain.Shipments;
using CustomCADs.Delivery.Domain.Shipments.Reads;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Shipments;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Delivery.Persistence.Shipments.Reads;

public class ShipmentReads(DeliveryContext context) : IShipmentReads
{
    public async Task<ShipmentResult> AllAsync(ShipmentQuery query, bool track = true, CancellationToken ct = default)
    {
        IQueryable<Shipment> queryable = context.Shipments
            .WithFilter(query.ClientId, query.ShipmentStatus)
            .WithSorting(query.Sorting ?? new());

        int count = await queryable.CountAsync(ct).ConfigureAwait(false);
        Shipment[] shipments = await queryable
            .WithPagination(query.Page, query.Limit)
            .ToArrayAsync(ct)
            .ConfigureAwait(false);

        return new(count, shipments);
    }

    public async Task<Shipment?> SingleByIdAsync(ShipmentId id, bool track = true, CancellationToken ct = default)
        => await context.Shipments
            .WithTracking(false)
            .FirstOrDefaultAsync(s => s.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<bool> ExistsByIdAsync(ShipmentId id, CancellationToken ct = default)
        => await context.Shipments
            .WithTracking(false)
            .AnyAsync(s => s.Id == id, ct)
            .ConfigureAwait(false);

    public async Task<int> CountAsync(CancellationToken ct = default)
        => await context.Shipments
            .WithTracking(false)
            .CountAsync(ct)
            .ConfigureAwait(false);
}
