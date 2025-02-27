namespace CustomCADs.Shared.Speedy.API.Dtos.PickupOrder;

public record PickupOrderDto(
    long Id,
    string[] ShipmentIds,
    string? PickupPeriodFrom,
    string? PickupPeriodTo
);
