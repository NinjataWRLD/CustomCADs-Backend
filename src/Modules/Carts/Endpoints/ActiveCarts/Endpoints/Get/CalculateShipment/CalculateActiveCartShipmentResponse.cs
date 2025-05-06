namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Get.CalculateShipment;

public record CalculateActiveCartShipmentResponse(
    string Service,
    double Total,
    string Currency,
    DateOnly PickupDate,
    DateTimeOffset DeliveryDeadline
);
