namespace CustomCADs.Speedy.Http.Endpoints.ShipmentEndpoints.BarcodeInformation;

using Dtos.ShipmentParcels;

internal record BarcodeInformationRequest(
	string UserName,
	string Password,
	ShipmentParcelRefDto Parcel,
	string? Language,
	long? ClientSystemId
);
