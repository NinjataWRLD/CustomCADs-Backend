namespace CustomCADs.Speedy.Http.Dtos.ShipmentSenderAndRecipient.ShipmentAddress;

/// <param name="CountryId">
///     Country ISO code. Used for all address types.
///     <br />
///     Required: No.
/// </param>
/// <param name="PostCode">
///     Postcode. Used for all address types.
///     <br />
///     Required: No.
/// </param>
/// <param name="SiteId">
///     Site id. Used for all address types.
///     <br />
///     Required: No.
/// </param>
/// <param name="SiteType">
///     Site type. Used for addresses of type 1 (local address).
///     <br />
///     Required: No.
/// </param>
/// <param name="SiteName">
///     Site name. Used for all address types.
///     <br />
///     Required: No.
/// </param>
/// <param name="ComplexId">
///     Complex identifier. Used for addresses of type 1 (local address).
///     <br />
///     Required: No.
/// </param>
/// <param name="ComplexType">
///     Complex type. Used for addresses of type 1 (local address).
///     <br />
///     Required: No.
/// </param>
/// <param name="ComplexName">
///     Complex name. Used for addresses of type 1 (local address).
///     <br />
///     Required: No.
/// </param>
/// <param name="StreetId">
///     Street identifier. Used for addresses of type 1 (local address).
///     <br />
///     Required: No.
/// </param>
/// <param name="StreetType">
///     Street type. Used for addresses of type 1 (local address).
///     <br />
///     Required: No.
/// </param>
/// <param name="StreetName">
///     Street name. Used for addresses of type 1 (local address).
///     <br />
///     Required: No.
/// </param>
/// <param name="StreetNo">
///     Street number. Used for addresses of type 1 (local address).
///     <br />
///     Required: No.
/// </param>
/// <param name="BlockNo">
///     Block No. Used for addresses of type 1 (local address).
///     <br />
///     Required: No.
/// </param>
/// <param name="EntranceNo">
///     Entrance. Used for addresses of type 1 (local address).
///     <br />
///     Required: No.
/// </param>
/// <param name="FloorNo">
///     Floor. Used for addresses of type 1 (local address).
///     <br />
///     Required: No.
/// </param>
/// <param name="ApartmentNo">
///     Apartment. Used for addresses of type 1 (local address).
///     <br />
///     Required: No.
/// </param>
/// <param name="PoiId">
///     Point of interest identifier. Used for addresses of type 1 (local address).
///     <br />
///     Required: No.
/// </param>
/// <param name="AddressNote">
///     Address note. Used for addresses of type 1 (local address).
///     <br />
///     Required: No.
/// </param>
/// <param name="X">
///     GIS coordinates - X (longitude).
///     <br />
///     Required: No.
/// </param>
/// <param name="Y">
///     GIS coordinates - Y (latitude).
///     <br />
///     Required: No.
/// </param>
internal record ShipmentAddressDto(
	int? CountryId,
	string? PostCode,

	long? SiteId,
	string? SiteType,
	string? SiteName,

	long? ComplexId,
	string? ComplexType,
	string? ComplexName,

	long? StreetId,
	string? StreetType,
	string? StreetName,

	string? StreetNo,
	string? BlockNo,
	string? EntranceNo,
	string? FloorNo,
	string? ApartmentNo,

	long? PoiId,
	string? AddressNote,
	double? X,
	double? Y
);
