namespace CustomCADs.Speedy.Http.Dtos.CalculationContent;

using ShipmentContent.ShipmentParcel;

internal record CalculationContentDto(
	int? ParcelsCount,
	double? TotalWeight,
	bool? Documents,
	bool? Palletized,
	ShipmentParcelDto[]? Parcels
);
