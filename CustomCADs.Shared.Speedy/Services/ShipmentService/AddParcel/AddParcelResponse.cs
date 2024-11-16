namespace CustomCADs.Shared.Speedy.Services.ShipmentService.AddParcel;

using Dtos.ShipmentParcels;

public record AddParcelResponse(
    CreatedShipmentParcelDto Parcel,
    ErrorDto? Error
);
