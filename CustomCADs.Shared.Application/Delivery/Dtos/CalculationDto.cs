namespace CustomCADs.Shared.Application.Delivery.Dtos;

public record CalculationDto(
    string Service,
    ShipmentPriceDto Price,
    DateOnly PickupDate,
    DateTime DeliveryDeadline
);
