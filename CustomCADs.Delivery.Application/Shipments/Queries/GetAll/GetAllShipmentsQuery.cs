using CustomCADs.Delivery.Domain.Shipments.Enums;
using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Delivery.Application.Shipments.Queries.GetAll;

public sealed record GetAllShipmentsQuery(
    AccountId? ClientId = null,
    ShipmentStatus? ShipmentStatus = null,
    ShipmentSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
) : IQuery<Result<GetAllShipmentsDto>>;
