namespace CustomCADs.Orders.Endpoints.Designer.Get.Accepted;

public record GetAcceptedOrdersDto(
    Guid Id,
    string Name,
    string DeliveryType,
    string OrderDate,
    string BuyerName
);