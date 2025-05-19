using CustomCADs.Shared.Speedy.Services.Models.Shipment.Parcel;

namespace CustomCADs.Shared.Speedy.Services.Print.Models;

public record ParcelToPrintModel(
	ShipmentParcelRefModel Parcel,
	ParcelToPrintAdditionalBarcodeModel? AdditionalBarcode
);
