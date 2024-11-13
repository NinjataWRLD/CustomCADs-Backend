using CustomCADs.Orders.Domain.GalleryOrders.Enums;
using CustomCADs.Shared.Core.Domain.Enums;

namespace CustomCADs.Orders.Domain.GalleryOrders.ValueObjects;

public record GalleryOrderSorting(
    GalleryOrderSortingType Type = GalleryOrderSortingType.OrderDate,
    SortingDirection Direction = SortingDirection.Descending
);
