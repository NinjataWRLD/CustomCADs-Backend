namespace CustomCADs.Shared.Speedy.API.Endpoints.ShipmentEndpoints.BarcodeInformation;

using Dtos.ParcelToPrint;
using Dtos.Shipment.Primary;

public record BarcodeInformationResponse(
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
