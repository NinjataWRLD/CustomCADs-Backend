namespace CustomCADs.Shared.Speedy.Services.ShipmentService.CancelShipment;

public record CancelShipmentRequest(
    string UserName,
    string Password,
    string ShipmentId,
    string Comment,
    string? Language,
    long? ClientSystemId
);
