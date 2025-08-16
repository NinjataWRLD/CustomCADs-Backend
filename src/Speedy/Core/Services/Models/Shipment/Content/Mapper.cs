using CustomCADs.Speedy.Http.Dtos.ShipmentContent;
using CustomCADs.Speedy.Http.Dtos.ShipmentContent.ShipmentParcel;

namespace CustomCADs.Speedy.Core.Services.Models.Shipment.Content;

internal static class Mapper
{
	internal static ShipmentContentDto ToDto(this ShipmentContentModel model)
		=> new(
			Contents: model.Contents,
			Package: model.Package,
			ParcelsCount: model.ParcelsCount,
			TotalWeight: model.TotalWeight,
			Documents: model.Documents,
			Palletized: model.Palletized,
			Parcels: [.. model.Parcels?.Select(p => p.ToDto()) ?? []],
			PendingParcels: model.PendingParcels,
			ExciseGoods: model.ExciseGoods,
			Iq: model.Iq,
			GoodsValue: model.GoodsValue,
			GoodsValueCurrencyCode: model.GoodsValueCurrencyCode,
			UitCode: model.UitCode
		);

	internal static ShipmentParcelDto ToDto(this ShipmentParcelModel model)
		=> new(
			Weight: model.Weight,
			Id: model.Id,
			SeqNo: model.SeqNo,
			PackageUniqueNumber: model.PackageUniqueNumber,
			Ref1: model.Ref1,
			Ref2: model.Ref2,
			Size: model.Size?.ToDto(),
			PickupExternalCarrierParcelNumber: model.PickupExternalCarrierParcelNumber?.ToDto(),
			DeliveryExternalCarrierParcelNumber: model.DeliveryExternalCarrierParcelNumber?.ToDto()
		);

	internal static ShipmentParcelSizeDto ToDto(this ShipmentParcelSizeModel model)
		=> new(
			Width: model.Width,
			Depth: model.Depth,
			Height: model.Height
		);

	internal static ExternalCarrierParcelNumberDto ToDto(this ExternalCarrierParcelNumberModel model)
		=> new(
			ExternalCarrier: model.ExternalCarrier,
			ParcelNumber: model.ParcelNumber
		);

	internal static ShipmentContentModel ToModel(this ShipmentContentDto dto)
		=> new(
			Contents: dto.Contents,
			Package: dto.Package,
			ParcelsCount: dto.ParcelsCount,
			TotalWeight: dto.TotalWeight,
			Documents: dto.Documents,
			Palletized: dto.Palletized,
			Parcels: [.. dto.Parcels?.Select(p => p.ToModel()) ?? []],
			PendingParcels: dto.PendingParcels,
			ExciseGoods: dto.ExciseGoods,
			Iq: dto.Iq,
			GoodsValue: dto.GoodsValue,
			GoodsValueCurrencyCode: dto.GoodsValueCurrencyCode,
			UitCode: dto.UitCode
		);

	internal static ShipmentParcelModel ToModel(this ShipmentParcelDto dto)
		=> new(
			Weight: dto.Weight,
			Id: dto.Id,
			SeqNo: dto.SeqNo,
			PackageUniqueNumber: dto.PackageUniqueNumber,
			Ref1: dto.Ref1,
			Ref2: dto.Ref2,
			Size: dto.Size?.ToModel(),
			PickupExternalCarrierParcelNumber: dto.PickupExternalCarrierParcelNumber?.ToModel(),
			DeliveryExternalCarrierParcelNumber: dto.DeliveryExternalCarrierParcelNumber?.ToModel()
		);

	internal static ShipmentParcelSizeModel ToModel(this ShipmentParcelSizeDto dto)
		=> new(
			Width: dto.Width,
			Depth: dto.Depth,
			Height: dto.Height
		);

	internal static ExternalCarrierParcelNumberModel ToModel(this ExternalCarrierParcelNumberDto dto)
		=> new(
			ExternalCarrier: dto.ExternalCarrier,
			ParcelNumber: dto.ParcelNumber
		);
}
