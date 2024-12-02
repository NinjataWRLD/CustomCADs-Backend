namespace CustomCADs.Orders.Endpoints.Designer.Get.Completed;

public record GetCompletedOrdersDto(
    Guid Id,
    string Name,
    string DeliveryType,
    string OrderDate,
    string BuyerName
);