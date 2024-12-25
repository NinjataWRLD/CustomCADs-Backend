namespace CustomCADs.Carts.Endpoints.Carts.Get.CalculateShipment;

public record CalculateCartShipmentResponse(
    string Service,
    double Total,
    string Currency,
    string PickupDate,
    string DeliveryDeadline
);
