namespace CustomCADs.Shared.Speedy.Dtos.Shipment.Primary;

using Enums;

public record PrimaryShipmentDto(
    string Id,
    PrimaryShipmentType Type
);