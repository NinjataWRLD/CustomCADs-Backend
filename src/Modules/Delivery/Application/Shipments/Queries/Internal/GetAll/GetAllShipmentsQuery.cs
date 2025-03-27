using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetAll;

public sealed record GetAllShipmentsQuery(
    Pagination Pagination,
    AccountId? ClientId = null,
    ShipmentSorting? Sorting = null
) : IQuery<Result<GetAllShipmentsDto>>;
