namespace CustomCADs.Shared.Application.Delivery.Dtos;

public record CalculationDto(
    int ServiceId,
    ShipmentPriceDto Price,
    DateOnly PickupDate,
    string DeliveryDeadline
);
