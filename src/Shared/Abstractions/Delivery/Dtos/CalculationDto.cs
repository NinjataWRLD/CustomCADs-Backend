namespace CustomCADs.Shared.Abstractions.Delivery.Dtos;

public record CalculationDto(
    string Service,
    ShipmentPriceDto Price,
    DateOnly PickupDate,
    DateTimeOffset DeliveryDeadline
);
