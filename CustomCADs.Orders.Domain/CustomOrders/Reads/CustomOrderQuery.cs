using CustomCADs.Orders.Domain.CustomOrders.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Domain.CustomOrders.Reads;

public record CustomOrderQuery(
    string? DeliveryType = null,
    string? OrderStatus = null,
    UserId? BuyerId = null,
    UserId? DesignerId = null,
    string? Name = null,
    CustomOrderSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
);
