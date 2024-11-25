using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Delivery.Domain.Shipments.Reads;

public record ShipmentQuery(
    UserId? ClientId = null,
    string? ShipmentStatus = null,
    ShipmentSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
);
