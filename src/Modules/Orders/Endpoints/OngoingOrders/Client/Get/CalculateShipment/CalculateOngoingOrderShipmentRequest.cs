namespace CustomCADs.Orders.Endpoints.OngoingOrders.Client.Get.CalculateShipment;

public record CalculateOngoingOrderShipmentRequest(
    Guid Id,
    int Count,
    double Weight,
    string Country,
    string City
);
