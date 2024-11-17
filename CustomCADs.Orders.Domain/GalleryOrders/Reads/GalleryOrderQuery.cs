using CustomCADs.Orders.Domain.Common.Enums;
using CustomCADs.Orders.Domain.GalleryOrders.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Domain.GalleryOrders.Reads;

public record GalleryOrderQuery(
    DeliveryType? DeliveryType = null,
    UserId? BuyerId = null,
    GalleryOrderSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
);
