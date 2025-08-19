namespace CustomCADs.Speedy.Http.Endpoints.ShipmentEndpoints.BarcodeInformation;

using Dtos.ParcelToPrint;
using Dtos.Shipment.Primary;

internal record BarcodeInformationResponse(
	LabelInfoDto LabelInfo,
	PrimaryShipmentDto PrimaryShipment,
	string PrimaryParcelId,
	string ReturnShipmentId,
	string ReturnParcelId,
	string RedirectShipmentId,
	string RedirectParcelId,
	string InitialShipmentId,
	string InitialParcelId,
	ErrorDto? Error
);
