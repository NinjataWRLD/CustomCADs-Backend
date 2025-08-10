using CustomCADs.Delivery.Domain.Repositories.Reads;
using CustomCADs.Delivery.Domain.Shipments;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Delivery;
using CustomCADs.Shared.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Delivery.Persistence.Repositories.Shipments;

public sealed class Reads(DeliveryContext context) : IShipmentReads
{
	public async Task<Result<Shipment>> AllAsync(ShipmentQuery query, bool track = true, CancellationToken ct = default)
	{
		IQueryable<Shipment> queryable = context.Shipments
			.WithFilter(query.CustomerId)
			.WithSorting(query.Sorting ?? new());

		int count = await queryable.CountAsync(ct).ConfigureAwait(false);
		Shipment[] shipments = await queryable
			.WithPagination(query.Pagination)
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
