namespace CustomCADs.Speedy.Http.Dtos.Site;

/// <param name="Id">
///     Site id.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="CountryId">
///     Country id for the site.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="MainSiteId">
///     Main site id for the site.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Type">
///     Site type (local language).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="TypeEn">
///     Site type (English).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Name">
///     Site name (local language).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="NameEn">
///     Site name (English).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Municipality">
///     Municipality (local language).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="MunicipalityEn">
///     Municipality (English).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Region">
///     Region (local language).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="RegionEn">
///     Region (English).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="PostCode">
///     Postcode for the site.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="AddressNomenclature">
///     Address nomenclature type for the site.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="X">
///     X coordinate for the site.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Y">
///     Y coordinate for the site.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ServingDays">
///     Serving days for the site.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ServingOfficeid">
///     Serving office id for the site.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ServingHubOfficeId">
///     Serving hub office id for the site.
///     <br />
///     Required: Yes.
/// </param>
internal record SiteDto(
	long Id,
	int CountryId,
	long MainSiteId,
	string Type,
	string TypeEn,
	string Name,
	string NameEn,
	string Municipality,
	string MunicipalityEn,
	string Region,
	string RegionEn,
	string PostCode,
	int AddressNomenclature,
	double X,
	double Y,
	string ServingDays,
	int ServingOfficeid,
	int ServingHubOfficeId
);
