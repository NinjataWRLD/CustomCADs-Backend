using CustomCADs.Speedy.Http.Dtos.ParcelToPrint;
using CustomCADs.Speedy.Core.Models.Shipment.Parcel;
using CustomCADs.Speedy.Core.Contracts.Print;

namespace CustomCADs.Speedy.Core.Services.Print;

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
