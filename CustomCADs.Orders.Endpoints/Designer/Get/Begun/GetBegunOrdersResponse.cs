namespace CustomCADs.Orders.Endpoints.Designer.Get.Begun;

public record GetBegunOrdersResponse(
    int Count,
    ICollection<GetBegunOrdersDto> Orders
);
