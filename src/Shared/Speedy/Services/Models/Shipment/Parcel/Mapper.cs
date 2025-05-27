using CustomCADs.Shared.Speedy.API.Dtos.ShipmentParcels;

namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Parcel;

internal static class Mapper
{
	internal static ShipmentParcelRefDto ToDto(this ShipmentParcelRefModel model)
		=> new(
			Id: model.Id,
			ExternalCarrierParcelNumber: model.ExternalCarrierParcelNumber,
			FullBarcode: model.FullBarcode
		);
}
