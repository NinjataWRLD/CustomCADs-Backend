namespace CustomCADs.Orders.Endpoints.OngoingOrders.Client.Get.CalculateShipment;

public record CalculateOngoingOrderShipmentResponse(
    string Service,
    double Total,
    string Currency,
    string PickupDate,
    string DeliveryDeadline
);
