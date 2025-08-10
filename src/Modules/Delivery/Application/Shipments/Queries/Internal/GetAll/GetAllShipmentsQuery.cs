using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetAll;

public sealed record GetAllShipmentsQuery(
	Pagination Pagination,
	AccountId? CustomerId = null,
	ShipmentSorting? Sorting = null
) : IQuery<Result<GetAllShipmentsDto>>;
