using CustomCADs.Speedy.Core.Services.Models.Shipment.Primary;

namespace CustomCADs.Speedy.Core.Services.Shipment.Models;

public record BarcodeInformationModel(
	LabelInfoModel LabelInfo,
	PrimaryShipmentModel PrimaryShipment,
	string PrimaryParcelId,
	string ReturnShipmentId,
	string ReturnParcelId,
	string RedirectShipmentId,
	string RedirectParcelId,
	string InitialShipmentId,
	string InitialParcelId
);
