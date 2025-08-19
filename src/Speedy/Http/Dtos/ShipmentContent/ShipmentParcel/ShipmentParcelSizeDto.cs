namespace CustomCADs.Speedy.Http.Dtos.ShipmentContent.ShipmentParcel;

/// <param name="Width">
/// 	Parcel width
/// 	<br />
/// 	Required: Yes
/// </param>
/// <param name="Depth">
/// 	Parcel depth
/// 	<br />
/// 	Required: Yes
/// </param>
/// <param name="Height">
/// 	Parcel height
/// 	<br />
/// 	Required: Yes
/// </param>
internal record ShipmentParcelSizeDto(
	int Width,
	int Depth,
	int Height
);
