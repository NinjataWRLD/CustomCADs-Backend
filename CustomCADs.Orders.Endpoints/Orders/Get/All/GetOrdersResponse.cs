namespace CustomCADs.Orders.Endpoints.Orders.Get.All;

public record GetOrdersResponse(
    int Count,
    ICollection<GetOrdersDto> Orders
);
