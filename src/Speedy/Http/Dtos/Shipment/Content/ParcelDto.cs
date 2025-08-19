namespace CustomCADs.Speedy.Http.Dtos.Shipment.Content;

using ShipmentContent.ShipmentParcel;

/// <summary>
/// DTO representing a parcel within shipment content.
/// </summary>
/// <param name="Id">Unique identifier for the parcel.</param>
/// <param name="SeqNo">Sequence number of the parcel.</param>
/// <param name="PackageUniqueNumber">Unique number for the package.</param>
/// <param name="DeclaredSize">Declared size of the parcel.</param>
/// <param name="MeasuredSize">Measured size of the parcel.</param>
/// <param name="CalculationSize">Size used for calculation purposes.</param>
/// <param name="DeclaredWeight">Declared weight of the parcel.</param>
/// <param name="MeasuredWeight">Measured weight of the parcel.</param>
/// <param name="CalculationWeight">Weight used for calculation purposes.</param>
/// <param name="ExternalCarrierParcelNumbers">External carrier parcel numbers.</param>
/// <param name="BaseType">Base type of the parcel.</param>
/// <param name="Size">Size description.</param>
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
