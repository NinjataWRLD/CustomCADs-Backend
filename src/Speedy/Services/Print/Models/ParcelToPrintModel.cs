using CustomCADs.Speedy.Services.Models.Shipment.Parcel;

namespace CustomCADs.Speedy.Services.Print.Models;

public record ParcelToPrintModel(
	ShipmentParcelRefModel Parcel,
	ParcelToPrintAdditionalBarcodeModel? AdditionalBarcode
);
