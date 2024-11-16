namespace CustomCADs.Shared.Speedy.API.Endpoints.ShipmentEndpoints.CancelShipment;

public record CancelShipmentRequest(
    string UserName,
    string Password,
    string ShipmentId,
    string Comment,
    string? Language,
    long? ClientSystemId
);
