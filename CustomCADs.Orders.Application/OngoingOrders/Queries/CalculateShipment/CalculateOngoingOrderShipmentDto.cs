namespace CustomCADs.Orders.Application.OngoingOrders.Queries.CalculateShipment;

public record CalculateOngoingOrderShipmentDto(
    double Total,
    string Currency,
    string Service,
    DateOnly PickupDate,
    DateTime DeliveryDeadline
);