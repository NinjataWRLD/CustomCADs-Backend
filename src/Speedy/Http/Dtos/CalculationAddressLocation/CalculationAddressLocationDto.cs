namespace CustomCADs.Speedy.Http.Dtos.CalculationAddressLocation;

/// <param name="CountryId">
/// 	Country ISO code. If not provided, local country is assumed. Used for all address types.
/// 	<br />
/// 	Required: Not required.
/// </param>
/// <param name="StateId">
///		Country state. Used for addresses of type 2 (foreign address).
/// 	<br />
///		Required: Required, if country supports states
/// </param>
/// <param name="SiteId">
///		Site id. Used for all address types.
/// 	<br />
///		Required: Required, if country has full site nomenclature and pair (siteType, siteName) is not provided.
/// </param>
/// <param name="SiteType">
///		Site type can be used in conjunction with countryId and siteName to find unique site. Used for addresses of type 1 (local address).
/// 	<br />
///		Required: Forbidden, if siteId is provided. Otherwise, is not mandatory. Max 20 characters
/// </param>
/// <param name="SiteName">
///		Site type can be used in conjunction with countryId and siteName to find unique site. Used for all address types.
/// 	<br />
///		Required: Forbidden, if siteId is provided. Otherwise, is not mandatory. Max 50 characters
/// </param>
/// <param name="PostCode">
///		Can be used in conjunction with countryId to find unique site. Used for all address types.
/// 	<br />
///		Required: Required if country requires postcode for addresses
/// </param>
internal record CalculationAddressLocationDto(
	int? CountryId,
	string? StateId,
	long? SiteId,
	string? SiteType,
	string? SiteName,
	string? PostCode
);
