namespace CustomCADs.Speedy.Http.Dtos.Shipment.Content;

using ShipmentContent.ShipmentParcel;

internal record ParcelDto(
	string Id,
	int SeqNo,
	long PackageUniqueNumber,
	ShipmentParcelSizeDto DeclaredSize,
	ShipmentParcelSizeDto MeasuredSize,
	ShipmentParcelSizeDto CalculationSize,
	double DeclaredWeight,
	double MeasuredWeight,
	double CalculationWeight,
	string[] ExternalCarrierParcelNumbers,
	string BaseType,
	string Size
);
