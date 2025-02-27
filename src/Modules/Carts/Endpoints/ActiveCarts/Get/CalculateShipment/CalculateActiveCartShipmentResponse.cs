namespace CustomCADs.Carts.Endpoints.ActiveCarts.Get.CalculateShipment;

public record CalculateActiveCartShipmentResponse(
    string Service,
    double Total,
    string Currency,
    string PickupDate,
    string DeliveryDeadline
);
