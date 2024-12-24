namespace CustomCADs.Orders.Endpoints.Orders.Client.Get.CalculateShipment;

public record CalculateOrderShipmentResponse(
    double Total,
    string Currency,
    string PickupDate,
    string DeliveryDeadline
);
