namespace CustomCADs.Orders.Endpoints.Orders.Client.Get.CalculateShipment;

public record CalculateOrderShipmentRequest(
    Guid Id,
    double Weight,
    string Country,
    string City
);
