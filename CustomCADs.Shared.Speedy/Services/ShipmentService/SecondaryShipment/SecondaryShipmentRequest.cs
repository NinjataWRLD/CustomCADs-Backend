namespace CustomCADs.Shared.Speedy.Services.ShipmentService.SecondaryShipment;

using Enums;

public record SecondaryShipmentRequest(
    string UserName,
    string Password,
    ShipmentType[] Types,
    string? Language,
    long? ClientSystemId
);