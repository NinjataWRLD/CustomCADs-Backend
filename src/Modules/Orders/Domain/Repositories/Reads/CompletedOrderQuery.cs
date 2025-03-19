using CustomCADs.Orders.Domain.CompletedOrders.ValueObjects;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Orders.Domain.Repositories.Reads;

public record CompletedOrderQuery(
    Pagination Pagination,
    bool? Delivery = null,
    ProductId? ProductId = null,
    AccountId? BuyerId = null,
    AccountId? DesignerId = null,
    string? Name = null,
    CompletedOrderSorting? Sorting = null
);
