using CustomCADs.Speedy.Core.Models.Shipment.Primary;

namespace CustomCADs.Speedy.Core.Contracts.Shipment;

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
