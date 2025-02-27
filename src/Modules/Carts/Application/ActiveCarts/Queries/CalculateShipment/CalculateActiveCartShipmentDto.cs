namespace CustomCADs.Carts.Application.ActiveCarts.Queries.CalculateShipment;

public record CalculateActiveCartShipmentDto(
    double Total,
    string Currency,
    string Service,
    DateOnly PickupDate,
    DateTime DeliveryDeadline
);