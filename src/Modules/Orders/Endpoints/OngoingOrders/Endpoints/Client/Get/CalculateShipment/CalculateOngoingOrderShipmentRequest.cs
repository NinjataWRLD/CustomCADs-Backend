namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Get.CalculateShipment;

public record CalculateOngoingOrderShipmentRequest(
    Guid Id,
    int Count,
    string Country,
    string City,
    Guid CustomizationId
);
