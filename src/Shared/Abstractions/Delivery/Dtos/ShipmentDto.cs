namespace CustomCADs.Shared.Abstractions.Delivery.Dtos;

public record ShipmentDto(
    string Id,
    string[] ParcelIds,
    decimal Price,
    DateOnly PickupDate,
    DateTime DeliveryDeadline
);
