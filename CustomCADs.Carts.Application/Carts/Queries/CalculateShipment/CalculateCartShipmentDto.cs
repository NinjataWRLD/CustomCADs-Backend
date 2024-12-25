namespace CustomCADs.Carts.Application.Carts.Queries.CalculateShipment;

public record CalculateCartShipmentDto(
    double Total,
    string Currency,
    string Service,
    DateOnly PickupDate,
    DateTime DeliveryDeadline
);