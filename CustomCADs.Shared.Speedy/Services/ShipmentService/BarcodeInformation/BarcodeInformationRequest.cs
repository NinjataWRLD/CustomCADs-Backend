namespace CustomCADs.Shared.Speedy.Services.ShipmentService.BarcodeInformation;

using Dtos.ShipmentParcels;

public record BarcodeInformationRequest(
    string UserName,
    string Password,
    ShipmentParcelRefDto Parcel,
    string? Language,
    long? ClientSysytemId
);
