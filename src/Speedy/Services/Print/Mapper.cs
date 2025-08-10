using CustomCADs.Speedy.API.Dtos.ParcelToPrint;
using CustomCADs.Speedy.Services.Models.Shipment.Parcel;
using CustomCADs.Speedy.Services.Print.Models;

namespace CustomCADs.Speedy.Services.Print;

internal static class Mapper
{
	internal static ParcelToPrintDto ToDto(this ParcelToPrintModel model)
		=> new(
			Parcel: model.Parcel.ToDto(),
			AdditionalBarcode: model.AdditionalBarcode?.ToDto()
		);

	internal static ParcelToPrintAdditionalBarcodeDto ToDto(this ParcelToPrintAdditionalBarcodeModel model)
		=> new(
			Value: model.Value,
			Format: model.Format,
			Label: model.Label
		);
}
