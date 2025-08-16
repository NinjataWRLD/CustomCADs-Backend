namespace CustomCADs.Speedy.Http.Dtos.CalculationAddressLocation;

internal record CalculationAddressLocationDto(
	int? CountryId,
	string? StateId,
	long? SiteId,
	string? SiteType,
	string? SiteName,
	string? PostCode
);
