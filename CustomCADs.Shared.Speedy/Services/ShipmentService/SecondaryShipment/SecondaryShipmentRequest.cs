namespace CustomCADs.Shared.Speedy.Services.ShipmentService.SecondaryShipment;

public record SecondaryShipmentRequest(
    string UserName,
    string Password,
    ShipmentType[] Types,
    string? Language,
    long? ClientSystemId
);