namespace CustomCADs.Orders.Endpoints.OngoingOrders.Client.Get.CalculateShipment;

public record CalculateOngoingOrderShipmentRequest(
    Guid Id,
    double Weight,
    string Country,
    string City
);
