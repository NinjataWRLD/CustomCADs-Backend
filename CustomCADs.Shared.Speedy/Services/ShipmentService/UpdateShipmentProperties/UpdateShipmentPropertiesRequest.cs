namespace CustomCADs.Shared.Speedy.Services.ShipmentService.UpdateShipmentProperties;

public record UpdateShipmentPropertiesRequest(
    string UserName,
    string Password,
    string Id,
    Dictionary<string, string> Properties,
    string? Language,
    long? ClientSystemId
);
