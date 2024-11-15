namespace CustomCADs.Shared.Speedy.Services.ShipmentService.AddParcel;

using CustomCADs.Shared.Speedy.Dtos.Errors;
using Dtos.ShipmentParcels;

public record AddParcelResponse(
    CreatedShipmentParcelDto Parcel,
    ErrorDto? Error
);
