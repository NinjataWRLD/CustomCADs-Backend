namespace CustomCADs.Shared.Speedy.Services.Pickup.Models;

public record PickupOrderModel(
    long Id,
    string[] ShipmentIds,
    DateTime? PickupPeriodFrom,
    DateTime? PickupPeriodTo
);
