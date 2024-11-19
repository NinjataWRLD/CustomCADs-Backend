using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Domain.Orders.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Domain.Orders.Reads;

public record OrderQuery(
    DeliveryType? DeliveryType = null,
    OrderStatus? OrderStatus = null,
    UserId? BuyerId = null,
    UserId? DesignerId = null,
    string? Name = null,
    OrderSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
);
