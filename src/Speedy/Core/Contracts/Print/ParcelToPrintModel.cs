using CustomCADs.Speedy.Core.Models.Shipment.Parcel;

namespace CustomCADs.Speedy.Core.Contracts.Print;

public record ParcelToPrintModel(
	ShipmentParcelRefModel Parcel,
	ParcelToPrintAdditionalBarcodeModel? AdditionalBarcode
);
