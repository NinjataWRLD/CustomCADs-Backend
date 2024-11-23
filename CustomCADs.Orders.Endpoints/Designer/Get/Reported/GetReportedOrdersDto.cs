namespace CustomCADs.Orders.Endpoints.Designer.Get.Reported;

public record GetReportedOrdersDto(
    Guid Id,
    string Name,
    string DeliveryType,
    string OrderDate,
    string BuyerName
);