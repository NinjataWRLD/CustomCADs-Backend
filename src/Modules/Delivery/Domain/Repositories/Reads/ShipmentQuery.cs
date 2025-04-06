using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Delivery.Domain.Repositories.Reads;

public record ShipmentQuery(
    Pagination Pagination,
    AccountId? CustomerId = null,
    ShipmentSorting? Sorting = null
);
