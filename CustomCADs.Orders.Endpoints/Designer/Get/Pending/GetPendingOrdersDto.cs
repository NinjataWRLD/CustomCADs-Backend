namespace CustomCADs.Orders.Endpoints.Designer.Get.Pending;

public record GetPendingOrdersDto(
    Guid Id,
    string Name,
    string DeliveryType,
    string OrderDate,
    string BuyerName
);