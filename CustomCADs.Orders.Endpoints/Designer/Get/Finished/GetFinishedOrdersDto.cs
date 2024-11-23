namespace CustomCADs.Orders.Endpoints.Designer.Get.Finished;

public record GetFinishedOrdersDto(
    Guid Id,
    string Name,
    string DeliveryType,
    string OrderDate,
    string BuyerName
);