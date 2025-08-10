namespace CustomCADs.Speedy.API.Endpoints.LocationEndpoints.Site.FindSite;

public record FindSiteRequest(
	string UserName,
	string Password,
	int CountryId,
	string? Language,
	long? ClientSystemId,
	string? Name,
	string? PostCode,
	string? Type,
	string? Municipality,
	string? Region
);
