namespace CustomCADs.Speedy.Core.Services.Models.Calculation.Content;

public record CalculationContentModel(
	int? ParcelsCount,
	double? TotalWeight,
	bool? Documents,
	bool? Palletized,
	ShipmentParcelModel[]? Parcels
);
