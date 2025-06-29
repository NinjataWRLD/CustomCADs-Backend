﻿namespace CustomCADs.Shared.Speedy.API.Dtos.CalculationContent;

using ShipmentContent.ShipmentParcel;

public record CalculationContentDto(
	int? ParcelsCount,
	double? TotalWeight,
	bool? Documents,
	bool? Palletized,
	ShipmentParcelDto[]? Parcels
);
