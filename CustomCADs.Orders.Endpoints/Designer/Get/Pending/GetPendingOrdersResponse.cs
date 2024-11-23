namespace CustomCADs.Orders.Endpoints.Designer.Get.Pending;

public record GetPendingOrdersResponse(
    int Count,
    ICollection<GetPendingOrdersDto> Orders
);
