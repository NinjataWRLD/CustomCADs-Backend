namespace CustomCADs.Shared.Speedy.Services.ShipmentService.FinalizePendingShipment;

public record FinalizePendingShipmentRequest(
    string UserName,
    string Password,
    string ShipmentId,
    string? Language,
    long? ClientSystemId
);
