namespace CustomCADs.Orders.Endpoints.Designer.Get.Reported;

public record GetReportedOrdersResponse(
    int Count,
    ICollection<GetReportedOrdersDto> Orders
);
