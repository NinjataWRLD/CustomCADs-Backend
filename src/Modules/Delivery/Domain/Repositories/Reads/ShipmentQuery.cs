using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Delivery.Domain.Repositories.Reads;

public record ShipmentQuery(
	Pagination Pagination,
	AccountId? CustomerId = null,
	ShipmentSorting? Sorting = null
);
