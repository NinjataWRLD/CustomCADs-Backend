namespace CustomCADs.Orders.Endpoints.Designer.Get.Accepted;

public record GetAcceptedOrdersResponse(
    int Count,
    ICollection<GetAcceptedOrdersDto> Orders
);
