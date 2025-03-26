namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Get.CalculateShipment;

public record CalculateActiveCartShipmentResponse(
    string Service,
    double Total,
    string Currency,
    string PickupDate,
    string DeliveryDeadline
);
