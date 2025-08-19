namespace CustomCADs.Speedy.Http.Dtos.Shipment.Content;

/// <summary>
/// DTO representing shipment content details.
/// </summary>
/// <param name="ParcelsCount">Number of parcels in the shipment.</param>
/// <param name="DeclaredWeight">Declared weight of the shipment.</param>
/// <param name="MeasuredWeight">Measured weight of the shipment.</param>
/// <param name="CalculationWeight">Weight used for calculation purposes.</param>
/// <param name="Contents">Description of the contents.</param>
/// <param name="Package">Type of package.</param>
/// <param name="Documents">Indicates if documents are included.</param>
/// <param name="Palletized">Indicates if the shipment is palletized.</param>
/// <param name="Parcels">Parcel details.</param>
/// <param name="PendingParcels">Indicates if there are pending parcels.</param>
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
