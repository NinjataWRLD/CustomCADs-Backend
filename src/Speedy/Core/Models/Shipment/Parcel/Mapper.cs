using CustomCADs.Speedy.Http.Dtos.ShipmentParcels;

namespace CustomCADs.Speedy.Core.Models.Shipment.Parcel;

internal static class Mapper
{
	internal static ShipmentParcelRefDto ToDto(this ShipmentParcelRefModel model)
		=> new(
			Id: model.Id,
			ExternalCarrierParcelNumber: model.ExternalCarrierParcelNumber,
			FullBarcode: model.FullBarcode
		);
}
