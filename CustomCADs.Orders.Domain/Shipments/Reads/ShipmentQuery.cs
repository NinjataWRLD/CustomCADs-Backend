using CustomCADs.Orders.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Domain.Shipments.Reads;

public record ShipmentQuery(
    UserId? ClientId = null,
    string? ShipmentStatus = null,
    ShipmentSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
);
