using CustomCADs.Delivery.Domain.Shipments.Enums;
using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Delivery.Domain.Shipments.Reads;

public record ShipmentQuery(
    Pagination Pagination,
    AccountId? ClientId = null,
    ShipmentStatus? ShipmentStatus = null,
    ShipmentSorting? Sorting = null
);
