namespace CustomCADs.Orders.Endpoints.Designer.Get.Begun;

public record GetBegunOrdersDto(
    Guid Id,
    string Name,
    string DeliveryType,
    string OrderDate,
    string BuyerName
);