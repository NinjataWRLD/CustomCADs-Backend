namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Get.CalculateShipment;

public record CalculateOngoingOrderShipmentResponse(
    string Service,
    double Total,
    string Currency,
    string PickupDate,
    string DeliveryDeadline
);
