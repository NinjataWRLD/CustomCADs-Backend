namespace CustomCADs.Shared.Speedy.Dtos.ShipmentContent.ShipmentParcel;

public record ExternalCarrierParcelNumberDto(
    Carrier ExternalCarrier,
    string ParcelNumber
);