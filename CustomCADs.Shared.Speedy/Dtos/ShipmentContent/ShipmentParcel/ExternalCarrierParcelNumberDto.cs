namespace CustomCADs.Shared.Speedy.Dtos.ShipmentContent.ShipmentParcel;

using Enums;

public record ExternalCarrierParcelNumberDto(
    Carrier ExternalCarrier,
    string ParcelNumber
);