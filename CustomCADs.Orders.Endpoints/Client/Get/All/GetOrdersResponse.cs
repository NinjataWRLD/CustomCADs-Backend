namespace CustomCADs.Orders.Endpoints.Client.Get.All;

public record GetOrdersResponse(
    int Count,
    ICollection<GetOrdersDto> Orders
);
