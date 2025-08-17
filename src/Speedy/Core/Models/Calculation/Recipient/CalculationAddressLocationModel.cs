namespace CustomCADs.Speedy.Core.Models.Calculation.Recipient;

public record CalculationAddressLocationModel(
	int? CountryId,
	string? StateId,
	long? SiteId,
	string? SiteType,
	string? SiteName,
	string? PostCode
);
