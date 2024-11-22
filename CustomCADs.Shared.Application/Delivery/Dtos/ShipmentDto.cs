namespace CustomCADs.Shared.Application.Delivery.Dtos;

public record ShipmentDto(
    string Id,
    string[] ParcelIds,
    decimal Price,
    DateOnly PickupDate,
    DateTime DeliveryDeadline
);
