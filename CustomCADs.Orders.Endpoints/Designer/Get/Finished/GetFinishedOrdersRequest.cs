using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Shared.Core.Domain.Enums;

namespace CustomCADs.Orders.Endpoints.Designer.Get.Finished;

public record GetFinishedOrdersRequest(
    DeliveryType? DeliveryType = null,
    string? Name = null,
    OrderSortingType SortingType = OrderSortingType.OrderDate,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 20
);
