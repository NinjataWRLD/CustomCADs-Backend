namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentContent.ShipmentParcel;

using Enums;

public record ExternalCarrierParcelNumberDto(
    Carrier ExternalCarrier,
    string ParcelNumber
);