using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Domain.Orders.ValueObjects;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Orders.Domain.Orders.Reads;

public record OrderQuery(
    Pagination Pagination,
    bool? Delivery = null,
    OrderStatus? OrderStatus = null,
    ProductId? ProductId = null,
    AccountId? BuyerId = null,
    AccountId? DesignerId = null,
    string? Name = null,
    OrderSorting? Sorting = null
);
