namespace CustomCADs.Speedy.Http.Dtos.Shipment.Content;

internal record ContentDto(
	int ParcelsCount,
	double DeclaredWeight,
	double MeasuredWeight,
	double CalculationWeight,
	string Contents,
	string Package,
	bool Documents,
	bool Palletized,
	ParcelDto Parcels,
	bool PendingParcels
);
