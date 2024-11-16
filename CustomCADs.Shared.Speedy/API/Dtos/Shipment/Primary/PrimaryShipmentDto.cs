namespace CustomCADs.Shared.Speedy.API.Dtos.Shipment.Primary;

using Enums;

public record PrimaryShipmentDto(
    string Id,
    ShipmentType Type
);