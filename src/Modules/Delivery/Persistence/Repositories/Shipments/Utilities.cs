using CustomCADs.Delivery.Domain.Shipments;
using CustomCADs.Delivery.Domain.Shipments.Enums;
using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Core.Common.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Delivery.Persistence.Repositories.Shipments;

public static class Utilities
{
	public static IQueryable<Shipment> WithFilter(this IQueryable<Shipment> query, AccountId? clientId = null)
	{
		if (clientId is not null)
		{
			query = query.Where(s => s.BuyerId == clientId);
		}

		return query;
	}

	public static IQueryable<Shipment> WithSorting(this IQueryable<Shipment> query, ShipmentSorting sorting)
		=> sorting switch
		{
			{ Type: ShipmentSortingType.RequestedAt, Direction: SortingDirection.Ascending } => query.OrderBy(s => s.RequestedAt),
			{ Type: ShipmentSortingType.RequestedAt, Direction: SortingDirection.Descending } => query.OrderByDescending(s => s.RequestedAt),
			{ Type: ShipmentSortingType.Country, Direction: SortingDirection.Ascending } => query.OrderBy(s => s.Address.Country),
			{ Type: ShipmentSortingType.Country, Direction: SortingDirection.Descending } => query.OrderByDescending(s => s.Address.Country),
			{ Type: ShipmentSortingType.City, Direction: SortingDirection.Ascending } => query.OrderBy(s => s.Address.City),
			{ Type: ShipmentSortingType.City, Direction: SortingDirection.Descending } => query.OrderByDescending(s => s.Address.City),
			_ => query,
		};

	public static IQueryable<Shipment> WithPagination(this IQueryable<Shipment> query, int page = 1, int limit = 20)
		=> query.Skip((page - 1) * limit).Take(limit);
}
