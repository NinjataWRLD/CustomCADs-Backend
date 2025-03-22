using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Orders.Domain.OngoingOrders.ValueObjects;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Orders.Domain.Repositories.Reads;

public record OngoingOrderQuery(
    Pagination Pagination,
    bool? Delivery = null,
    OngoingOrderStatus? OrderStatus = null,
    ProductId? ProductId = null,
    AccountId? BuyerId = null,
    AccountId? DesignerId = null,
    string? Name = null,
    OngoingOrderSorting? Sorting = null
);
