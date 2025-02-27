namespace CustomCADs.Shared.Speedy.API.Endpoints.ShipmentEndpoints.BarcodeInformation;

using Dtos.ShipmentParcels;

public record BarcodeInformationRequest(
    string UserName,
    string Password,
    ShipmentParcelRefDto Parcel,
    string? Language,
    long? ClientSystemId
);
