namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentContent.ShipmentParcel;

public record ExternalCarrierParcelNumberDto(
    Carrier ExternalCarrier,
    string ParcelNumber
);