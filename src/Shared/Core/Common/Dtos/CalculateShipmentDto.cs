namespace CustomCADs.Shared.Core.Common.Dtos;

public record CalculateShipmentDto(
    double Total,
    string Currency,
    string Service,
    DateOnly PickupDate,
    DateTimeOffset DeliveryDeadline
);
