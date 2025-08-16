namespace CustomCADs.Speedy.Http.Dtos.ShipmentContent;

using ShipmentParcel;

internal record ShipmentContentDto(
	string Contents,
	string Package,
	int? ParcelsCount,
	double? TotalWeight,
	bool? Documents,
	bool? Palletized,
	ShipmentParcelDto[]? Parcels,
	bool? PendingParcels,
	bool? ExciseGoods,
	bool? Iq,
	double? GoodsValue,
	string? GoodsValueCurrencyCode,
	string? UitCode
);
