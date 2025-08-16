using CustomCADs.Speedy.Core.Services.Models.Shipment.Parcel;

namespace CustomCADs.Speedy.Core.Services.Print.Models;

public record ParcelToPrintModel(
	ShipmentParcelRefModel Parcel,
	ParcelToPrintAdditionalBarcodeModel? AdditionalBarcode
);
