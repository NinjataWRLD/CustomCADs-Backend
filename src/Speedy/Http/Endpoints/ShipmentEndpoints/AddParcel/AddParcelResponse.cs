namespace CustomCADs.Speedy.Http.Endpoints.ShipmentEndpoints.AddParcel;

using Dtos.ShipmentParcels;

internal record AddParcelResponse(
	CreatedShipmentParcelDto Parcel,
	ErrorDto? Error
);
