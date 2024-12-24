namespace CustomCADs.Orders.Application.Orders.Queries.CalculateShipment;

public record CalculateOrderShipmentDto(
    double Total,
    string Currency,
    string Service,
    DateOnly PickupDate,
    DateTime DeliveryDeadline
);