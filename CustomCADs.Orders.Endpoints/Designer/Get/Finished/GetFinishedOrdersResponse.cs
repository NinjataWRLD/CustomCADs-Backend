namespace CustomCADs.Orders.Endpoints.Designer.Get.Finished;

public record GetFinishedOrdersResponse(
    int Count,
    ICollection<GetFinishedOrdersDto> Orders
);
