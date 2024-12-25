namespace CustomCADs.Orders.Endpoints.Orders.Client.Get.CalculateShipment;

public record CalculateOrderShipmentResponse(
    string Service,
    double Total,
    string Currency,
    string PickupDate,
    string DeliveryDeadline
);
