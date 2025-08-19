namespace CustomCADs.Speedy.Http.Dtos.PointOfInterest;

/// <param name="Id">
///     Point of interest id.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="SiteId">
///     Site id for the point of interest.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Name">
///     Point of interest name (local language).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="NameEn">
///     Point of interest name (English).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Type">
///     Point of interest type (local language).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="TypeEn">
///     Point of interest type (English).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Address">
///     Address for the point of interest (local language).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="AddressEn">
///     Address for the point of interest (English).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="X">
///     X coordinate for the point of interest.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Y">
///     Y coordinate for the point of interest.
///     <br />
///     Required: Yes.
/// </param>
internal record PointOfInterestDto(
	long Id,
	long SiteId,
	string Name,
	string NameEn,
	string Type,
	string TypeEn,
	string Address,
	string AddressEn,
	double X,
	double Y
);
